using GovHub.Lib.Models;

namespace GovHub.Lib.DTOs;

/// <summary>
/// Response payload for chat messages
/// </summary>
public record ChatResponse
{
    public ChatMessage Message { get; init; } = null!;
    public List<Service>? SuggestedServices { get; init; }
    public TaskEntity? TaskCreated { get; init; }
    public bool RequiresAction { get; init; }
}
