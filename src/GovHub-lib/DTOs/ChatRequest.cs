namespace GovHub.Lib.DTOs;

/// <summary>
/// Request payload for chat messages
/// </summary>
public record ChatRequest
{
    public string UserId { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public Guid? CurrentTaskId { get; init; }
}
