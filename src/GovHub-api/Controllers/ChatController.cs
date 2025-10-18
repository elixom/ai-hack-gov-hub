using GovHub.Api.Services;
using GovHub.Lib.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GovHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatOrchestrator _orchestrator;
    private readonly ILogger<ChatController> _logger;

    public ChatController(ChatOrchestrator orchestrator, ILogger<ChatController> logger)
    {
        _orchestrator = orchestrator;
        _logger = logger;
    }

    /// <summary>
    /// Process a chat message
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ChatResponse>> PostMessage([FromBody] ChatRequest request)
    {
        try
        {
            var response = await _orchestrator.ProcessMessageAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing chat message");
            return StatusCode(500, new { error = "An error occurred processing your message" });
        }
    }

    /// <summary>
    /// Get conversation history for a user
    /// </summary>
    [HttpGet("history/{userId}")]
    public async Task<ActionResult> GetHistory(string userId)
    {
        try
        {
            var history = await _orchestrator.GetConversationHistoryAsync(userId);
            return Ok(history);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching conversation history");
            return StatusCode(500, new { error = "An error occurred fetching history" });
        }
    }

    /// <summary>
    /// Get current task for a user
    /// </summary>
    [HttpGet("task/{userId}")]
    public async Task<ActionResult> GetCurrentTask(string userId)
    {
        try
        {
            var task = await _orchestrator.GetCurrentTaskAsync(userId);
            return Ok(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching current task");
            return StatusCode(500, new { error = "An error occurred fetching task" });
        }
    }
}
