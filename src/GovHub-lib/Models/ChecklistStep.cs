using System.ComponentModel.DataAnnotations;

namespace GovHub.Lib.Models;

/// <summary>
/// Represents a single step in a task checklist
/// </summary>
public record ChecklistStep
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public Guid TaskId { get; init; }

    [Required]
    public int Order { get; init; }

    [Required]
    [MaxLength(500)]
    public string Description { get; init; } = string.Empty;

    public bool IsCompleted { get; set; } = false;

    public List<string> RequiredDocuments { get; init; } = new();

    public DateTime? CompletedAt { get; set; }
}
