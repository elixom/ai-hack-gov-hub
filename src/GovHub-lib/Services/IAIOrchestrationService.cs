using GovHub.Lib.Models.AI;

namespace GovHub.Lib.Services;

/// <summary>
/// Service for AI-powered intent classification and routing
/// Simulates Azure OpenAI function calling
/// </summary>
public interface IAIOrchestrationService
{
    /// <summary>
    /// Analyzes user message and determines which tool/action to execute
    /// </summary>
    Task<ToolChoice> ClassifyIntentAsync(string userMessage, string userId, Guid? currentTaskId = null);
}
