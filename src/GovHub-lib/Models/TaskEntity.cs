using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskStatus = GovHub.Lib.Enums.TaskStatus;

namespace GovHub.Lib.Models;

/// <summary>
/// Represents a user's task for completing a government service process
/// Includes temporal tracking for created, started, deadline, and completion times
/// </summary>
public record TaskEntity
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public string UserId { get; init; } = string.Empty;

    [Required]
    public Guid ServiceId { get; init; }

    [Required]
    public TaskStatus Status { get; set; } = TaskStatus.Pending;

    [Range(0, 100)]
    public int ProgressPercent { get; set; } = 0;

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? StartedAt { get; set; }

    public DateTime? Deadline { get; set; }

    public DateTime? CompletedAt { get; set; }

    public List<string> AttachedDocuments { get; set; } = new();

    [NotMapped]
    public Service? Service { get; set; }
}
