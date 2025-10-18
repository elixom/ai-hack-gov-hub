using System.ComponentModel.DataAnnotations;

namespace GovHub.Lib.Models;

/// <summary>
/// Represents a Jamaican government service that users can inquire about
/// </summary>
public record Service
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    public string Title { get; init; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string ShortDescription { get; init; } = string.Empty;

    public string[] Tags { get; init; } = Array.Empty<string>();

    [Required]
    [Url]
    public string CanonicalDeepLink { get; init; } = string.Empty;

    public Guid? WizardTemplateId { get; init; }
}
