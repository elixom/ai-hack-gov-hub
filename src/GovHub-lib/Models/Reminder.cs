using System.ComponentModel.DataAnnotations;

namespace GovHub.Lib.Models;

/// <summary>
/// Represents a reminder for a task
/// </summary>
public record Reminder
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public Guid TaskId { get; init; }

    [Required]
    public DateTime DueDate { get; init; }

    [Required]
    [MaxLength(500)]
    public string Message { get; init; } = string.Empty;

    public bool IsDismissed { get; set; } = false;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? DismissedAt { get; set; }
}
