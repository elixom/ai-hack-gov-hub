using System.ComponentModel.DataAnnotations;
using GovHub.Lib.Enums;

namespace GovHub.Lib.Models;

/// <summary>
/// Represents a real-time status tracker for a government service
/// </summary>
public record Tracker
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    public Guid TaskId { get; init; }

    [Required]
    [MaxLength(100)]
    public string Provider { get; init; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Reference { get; init; } = string.Empty;

    [Required]
    public TrackerStatus Status { get; set; } = TrackerStatus.NotStarted;

    [Required]
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? StatusMessage { get; set; }
}
