using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using GovHub.Lib.Enums;

namespace GovHub.Lib.Models;

/// <summary>
/// Represents a chat message in the conversation thread
/// </summary>
public record ChatMessage
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public string Text { get; init; } = string.Empty;

    [Required]
    public MessageRole Role { get; init; } = MessageRole.User;

    [Required]
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// JSON payload for rich content like service chips, checklists, etc.
    /// </summary>
    public string? RichContentJson { get; init; }

    public RichContentType? RichContentType { get; init; }

    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// Deserializes the rich content JSON into a typed object
    /// </summary>
    public T? GetRichContent<T>() where T : class
    {
        if (string.IsNullOrEmpty(RichContentJson))
            return null;

        try
        {
            return JsonSerializer.Deserialize<T>(RichContentJson);
        }
        catch
        {
            return null;
        }
    }
}
