using GovHub.Lib.Models.AI;

namespace GovHub.Lib.Services;

/// <summary>
/// Mock implementation of AI orchestration that simulates Azure OpenAI function calling
/// Uses pattern matching for intent classification
/// </summary>
public class AIOrchestrationService : IAIOrchestrationService
{
    public Task<ToolChoice> ClassifyIntentAsync(string userMessage, string userId, Guid? currentTaskId = null)
    {
        var lowerMessage = userMessage.ToLowerInvariant().Trim();

        // Check for commands first
        if (lowerMessage.StartsWith("/"))
        {
            return Task.FromResult(ParseCommand(lowerMessage));
        }

        // Service search patterns
        if (ContainsAny(lowerMessage, new[] { "fitness", "vehicle", "registration", "motor", "car", "inspection" }))
        {
            return Task.FromResult(new ToolChoice
            {
                ToolName = AvailableTools.ServiceSearch,
                Parameters = new Dictionary<string, string> { { "query", userMessage } },
                Reasoning = "User is inquiring about vehicle fitness or registration services"
            });
        }

        if (ContainsAny(lowerMessage, new[] { "nht", "housing", "loan", "mortgage", "property", "home" }))
        {
            return Task.FromResult(new ToolChoice
            {
                ToolName = AvailableTools.ServiceSearch,
                Parameters = new Dictionary<string, string> { { "query", userMessage } },
                Reasoning = "User is inquiring about NHT benefits or housing services"
            });
        }

        if (ContainsAny(lowerMessage, new[] { "taj", "tax", "trn", "revenue", "filing", "return" }))
        {
            return Task.FromResult(new ToolChoice
            {
                ToolName = AvailableTools.ServiceSearch,
                Parameters = new Dictionary<string, string> { { "query", userMessage } },
                Reasoning = "User is inquiring about tax services"
            });
        }

        // Document search patterns
        if (ContainsAny(lowerMessage, new[] { "document", "documents", "docs", "forms", "pdf", "show me" }))
        {
            return Task.FromResult(new ToolChoice
            {
                ToolName = AvailableTools.ShowDocuments,
                Parameters = new Dictionary<string, string> { { "query", userMessage } },
                Reasoning = "User is requesting documents or forms"
            });
        }

        // Tracker patterns
        if (ContainsAny(lowerMessage, new[] { "track", "tracker", "status", "check status", "monitor" }))
        {
            return Task.FromResult(new ToolChoice
            {
                ToolName = AvailableTools.QuickAttachTracker,
                Parameters = new Dictionary<string, string> { { "query", userMessage } },
                Reasoning = "User wants to track application status"
            });
        }

        // Reminder patterns
        if (ContainsAny(lowerMessage, new[] { "remind", "reminder", "alert", "notify" }))
        {
            return Task.FromResult(new ToolChoice
            {
                ToolName = AvailableTools.AddReminder,
                Parameters = new Dictionary<string, string> { { "offset", ExtractTimeOffset(lowerMessage) } },
                Reasoning = "User wants to set a reminder"
            });
        }

        // Start wizard patterns
        if (ContainsAny(lowerMessage, new[] { "start", "begin", "help me with", "guide me", "checklist" }))
        {
            return Task.FromResult(new ToolChoice
            {
                ToolName = AvailableTools.StartWizard,
                Parameters = new Dictionary<string, string> { { "query", userMessage } },
                Reasoning = "User wants to start a guided process"
            });
        }

        // Default to general query
        return Task.FromResult(new ToolChoice
        {
            ToolName = AvailableTools.GeneralQuery,
            Parameters = new Dictionary<string, string> { { "query", userMessage } },
            Reasoning = "General inquiry or conversation"
        });
    }

    private static ToolChoice ParseCommand(string command)
    {
        var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var cmd = parts[0].ToLowerInvariant();

        return cmd switch
        {
            "/new" => new ToolChoice
            {
                ToolName = AvailableTools.ServiceSearch,
                Parameters = new Dictionary<string, string>(),
                Reasoning = "User initiated new service search"
            },
            "/docs" => new ToolChoice
            {
                ToolName = AvailableTools.ShowDocuments,
                Parameters = new Dictionary<string, string> { { "query", string.Join(" ", parts.Skip(1)) } },
                Reasoning = "User requested document search via command"
            },
            "/track" => new ToolChoice
            {
                ToolName = AvailableTools.QuickAttachTracker,
                Parameters = new Dictionary<string, string>
                {
                    { "provider", parts.Length > 1 ? parts[1] : "" },
                    { "reference", parts.Length > 2 ? parts[2] : "" }
                },
                Reasoning = "User attached tracker via command"
            },
            "/remind" => new ToolChoice
            {
                ToolName = AvailableTools.AddReminder,
                Parameters = new Dictionary<string, string> { { "offset", string.Join(" ", parts.Skip(1)) } },
                Reasoning = "User set reminder via command"
            },
            _ => new ToolChoice
            {
                ToolName = AvailableTools.GeneralQuery,
                Parameters = new Dictionary<string, string> { { "query", command } },
                Reasoning = "Unknown command"
            }
        };
    }

    private static bool ContainsAny(string text, string[] keywords)
    {
        return keywords.Any(keyword => text.Contains(keyword));
    }

    private static string ExtractTimeOffset(string message)
    {
        // Simple extraction - look for common patterns
        if (message.Contains("1 hour")) return "1h";
        if (message.Contains("1 day")) return "1d";
        if (message.Contains("7 days") || message.Contains("week")) return "7d";
        if (message.Contains("1 minute")) return "1m";
        return "1d"; // default
    }
}
