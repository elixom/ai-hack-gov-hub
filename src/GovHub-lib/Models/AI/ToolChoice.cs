namespace GovHub.Lib.Models.AI;

/// <summary>
/// Represents the AI's intent classification and tool selection
/// </summary>
public record ToolChoice
{
    public string ToolName { get; init; } = string.Empty;
    public Dictionary<string, string> Parameters { get; init; } = new();
    public string Reasoning { get; init; } = string.Empty;
}

/// <summary>
/// Available tools for the AI orchestrator
/// </summary>
public static class AvailableTools
{
    public const string ServiceSearch = "ServiceSearch";
    public const string StartWizard = "StartWizard";
    public const string ShowDocuments = "ShowDocuments";
    public const string QuickAttachTracker = "QuickAttachTracker";
    public const string AddReminder = "AddReminder";
    public const string GeneralQuery = "GeneralQuery";
    public const string ToggleChecklistStep = "ToggleChecklistStep";
    public const string CreateTask = "CreateTask";
}
