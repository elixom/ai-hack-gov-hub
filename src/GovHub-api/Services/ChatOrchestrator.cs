using System.Text.Json;
using GovHub.Api.Hubs;
using GovHub.Lib.DTOs;
using GovHub.Lib.Enums;
using GovHub.Lib.Models;
using GovHub.Lib.Models.AI;
using GovHub.Lib.Services;
using Microsoft.AspNetCore.SignalR;
using TaskStatus = GovHub.Lib.Enums.TaskStatus;

namespace GovHub.Api.Services;

/// <summary>
/// Orchestrates chat interactions by coordinating AI, services, and SignalR updates
/// </summary>
public class ChatOrchestrator
{
    private readonly IAIOrchestrationService _aiService;
    private readonly IServiceCatalogService _catalogService;
    private readonly ITrackerService _trackerService;
    private readonly IReminderService _reminderService;
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly ILogger<ChatOrchestrator> _logger;

    // In-memory storage for demo
    private readonly Dictionary<Guid, TaskEntity> _tasks = new();
    private readonly Dictionary<string, List<ChatMessage>> _conversations = new();

    public ChatOrchestrator(
        IAIOrchestrationService aiService,
        IServiceCatalogService catalogService,
        ITrackerService trackerService,
        IReminderService reminderService,
        IHubContext<ChatHub> hubContext,
        ILogger<ChatOrchestrator> logger)
    {
        _aiService = aiService;
        _catalogService = catalogService;
        _trackerService = trackerService;
        _reminderService = reminderService;
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task<ChatResponse> ProcessMessageAsync(ChatRequest request)
    {
        _logger.LogInformation("Processing message from user {UserId}: {Message}", request.UserId, request.Message);

        // Store user message
        var userMessage = new ChatMessage
        {
            UserId = request.UserId,
            Text = request.Message,
            Role = MessageRole.User
        };
        AddMessageToConversation(request.UserId, userMessage);

        // Classify intent
        var toolChoice = await _aiService.ClassifyIntentAsync(request.Message, request.UserId, request.CurrentTaskId);
        _logger.LogInformation("AI classified intent as {ToolName}: {Reasoning}", toolChoice.ToolName, toolChoice.Reasoning);

        // Execute appropriate action
        return toolChoice.ToolName switch
        {
            AvailableTools.ServiceSearch => await HandleServiceSearch(request, toolChoice),
            AvailableTools.StartWizard => await HandleStartWizard(request, toolChoice),
            AvailableTools.ShowDocuments => await HandleShowDocuments(request, toolChoice),
            AvailableTools.QuickAttachTracker => await HandleAttachTracker(request, toolChoice),
            AvailableTools.AddReminder => await HandleAddReminder(request, toolChoice),
            AvailableTools.CreateTask => await HandleCreateTask(request, toolChoice),
            _ => await HandleGeneralQuery(request, toolChoice)
        };
    }

    private async Task<ChatResponse> HandleServiceSearch(ChatRequest request, ToolChoice tool)
    {
        var query = tool.Parameters.GetValueOrDefault("query", request.Message);
        var services = await _catalogService.SearchServicesAsync(query);

        var richContent = JsonSerializer.Serialize(services);
        var message = new ChatMessage
        {
            UserId = request.UserId,
            Text = services.Count > 0
                ? $"I found {services.Count} service(s) that match your inquiry. Select one to learn more:"
                : "I couldn't find any services matching your query. Try different keywords or ask me about vehicle fitness, NHT benefits, or tax services.",
            Role = MessageRole.Assistant,
            RichContentType = services.Count > 0 ? RichContentType.ServiceChips : null,
            RichContentJson = services.Count > 0 ? richContent : null
        };

        AddMessageToConversation(request.UserId, message);
        await NotifyUser(request.UserId, message);

        return new ChatResponse
        {
            Message = message,
            SuggestedServices = services
        };
    }

    private async Task<ChatResponse> HandleStartWizard(ChatRequest request, ToolChoice tool)
    {
        // Simplified wizard - in real implementation, this would have multi-step questions
        var message = new ChatMessage
        {
            UserId = request.UserId,
            Text = "Great! Let's get you started. First, which service are you interested in completing?",
            Role = MessageRole.Assistant,
            RichContentType = RichContentType.WizardQuickReplies,
            RichContentJson = JsonSerializer.Serialize(new[]
            {
                "Vehicle Fitness & Registration",
                "NHT Benefits",
                "TAJ Tax Registration"
            })
        };

        AddMessageToConversation(request.UserId, message);
        await NotifyUser(request.UserId, message);

        return new ChatResponse { Message = message };
    }

    private async Task<ChatResponse> HandleShowDocuments(ChatRequest request, ToolChoice tool)
    {
        // Mock document response
        var documents = new[]
        {
            new { Title = "Vehicle Fitness Application Form", Url = "https://www.gov.jm/forms/vehicle-fitness.pdf", Type = "PDF" },
            new { Title = "Motor Vehicle Inspection Guide", Url = "https://www.gov.jm/docs/inspection-guide.pdf", Type = "PDF" },
            new { Title = "NHT Benefits Overview", Url = "https://www.nht.gov.jm/benefits-guide.pdf", Type = "PDF" },
            new { Title = "TAJ Registration Requirements", Url = "https://www.jamaicatax.gov.jm/registration.html", Type = "HTML" }
        };

        var message = new ChatMessage
        {
            UserId = request.UserId,
            Text = $"I found {documents.Length} relevant documents for you:",
            Role = MessageRole.Assistant,
            RichContentType = RichContentType.DocumentCarousel,
            RichContentJson = JsonSerializer.Serialize(documents)
        };

        AddMessageToConversation(request.UserId, message);
        await NotifyUser(request.UserId, message);

        return new ChatResponse { Message = message };
    }

    private async Task<ChatResponse> HandleAttachTracker(ChatRequest request, ToolChoice tool)
    {
        if (request.CurrentTaskId == null)
        {
            var message = new ChatMessage
            {
                UserId = request.UserId,
                Text = "You need to create a task first before attaching a tracker. Would you like to start a service process?",
                Role = MessageRole.Assistant
            };

            AddMessageToConversation(request.UserId, message);
            await NotifyUser(request.UserId, message);

            return new ChatResponse { Message = message };
        }

        var provider = tool.Parameters.GetValueOrDefault("provider", "MOCK_FITNESS");
        var reference = tool.Parameters.GetValueOrDefault("reference", $"REF-{Guid.NewGuid().ToString()[..8].ToUpper()}");

        var tracker = await _trackerService.AttachTrackerAsync(request.CurrentTaskId.Value, provider, reference);

        var responseMessage = new ChatMessage
        {
            UserId = request.UserId,
            Text = $"Tracker attached! Provider: {provider}, Reference: {reference}",
            Role = MessageRole.Assistant,
            RichContentType = RichContentType.TrackerStatusPill,
            RichContentJson = JsonSerializer.Serialize(tracker)
        };

        AddMessageToConversation(request.UserId, responseMessage);
        await NotifyUser(request.UserId, responseMessage);
        await NotifyTrackerUpdate(request.UserId, tracker);

        return new ChatResponse { Message = responseMessage };
    }

    private async Task<ChatResponse> HandleAddReminder(ChatRequest request, ToolChoice tool)
    {
        if (request.CurrentTaskId == null)
        {
            var message = new ChatMessage
            {
                UserId = request.UserId,
                Text = "You need to create a task first before setting a reminder.",
                Role = MessageRole.Assistant
            };

            AddMessageToConversation(request.UserId, message);
            await NotifyUser(request.UserId, message);

            return new ChatResponse { Message = message };
        }

        var offset = tool.Parameters.GetValueOrDefault("offset", "1d");
        var reminderMessage = $"Reminder: Check your task progress";

        var reminder = await _reminderService.AddReminderWithOffsetAsync(
            request.CurrentTaskId.Value,
            reminderMessage,
            offset);

        var responseMessage = new ChatMessage
        {
            UserId = request.UserId,
            Text = $"Reminder set for {reminder.DueDate:g}",
            Role = MessageRole.System
        };

        AddMessageToConversation(request.UserId, responseMessage);
        await NotifyUser(request.UserId, responseMessage);

        return new ChatResponse { Message = responseMessage };
    }

    private async Task<ChatResponse> HandleCreateTask(ChatRequest request, ToolChoice tool)
    {
        // This would typically be called after a wizard flow
        var serviceId = tool.Parameters.TryGetValue("serviceId", out var sId) && Guid.TryParse(sId, out var parsedId)
            ? parsedId
            : Guid.Parse("11111111-1111-1111-1111-111111111111"); // Default to fitness

        var task = new TaskEntity
        {
            UserId = request.UserId,
            ServiceId = serviceId,
            Status = TaskStatus.InProgress,
            StartedAt = DateTime.UtcNow,
            Deadline = DateTime.UtcNow.AddDays(30)
        };

        _tasks[task.Id] = task;

        var message = new ChatMessage
        {
            UserId = request.UserId,
            Text = "Task created successfully! You can now track your progress in the right panel.",
            Role = MessageRole.System
        };

        AddMessageToConversation(request.UserId, message);
        await NotifyUser(request.UserId, message);
        await NotifyTaskUpdate(request.UserId, task);

        return new ChatResponse
        {
            Message = message,
            TaskCreated = task
        };
    }

    private async Task<ChatResponse> HandleGeneralQuery(ChatRequest request, ToolChoice tool)
    {
        var message = new ChatMessage
        {
            UserId = request.UserId,
            Text = "I'm here to help you with Jamaican government services. You can ask me about vehicle fitness & registration, NHT benefits, or tax services. What would you like to know?",
            Role = MessageRole.Assistant
        };

        AddMessageToConversation(request.UserId, message);
        await NotifyUser(request.UserId, message);

        return new ChatResponse { Message = message };
    }

    private void AddMessageToConversation(string userId, ChatMessage message)
    {
        if (!_conversations.ContainsKey(userId))
        {
            _conversations[userId] = new List<ChatMessage>();
        }

        _conversations[userId].Add(message);
    }

    private async Task NotifyUser(string userId, ChatMessage message)
    {
        await _hubContext.Clients.Group(userId).SendAsync("ReceiveMessage", message);
    }

    private async Task NotifyTaskUpdate(string userId, TaskEntity task)
    {
        await _hubContext.Clients.Group(userId).SendAsync("TaskUpdated", task);
    }

    private async Task NotifyTrackerUpdate(string userId, Tracker tracker)
    {
        await _hubContext.Clients.Group(userId).SendAsync("TrackerUpdated", tracker);
    }

    public async Task<List<ChatMessage>> GetConversationHistoryAsync(string userId)
    {
        return _conversations.TryGetValue(userId, out var messages)
            ? messages
            : new List<ChatMessage>();
    }

    public async Task<TaskEntity?> GetCurrentTaskAsync(string userId)
    {
        return _tasks.Values
            .Where(t => t.UserId == userId && t.Status != TaskStatus.Completed)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefault();
    }
}
