using GovHub.Lib.Models;

namespace GovHub.Lib.Services;

/// <summary>
/// Service for managing real-time status trackers for government applications
/// </summary>
public interface ITrackerService
{
    /// <summary>
    /// Attach a tracker to a task
    /// </summary>
    Task<Tracker> AttachTrackerAsync(Guid taskId, string provider, string referenceNumber);

    /// <summary>
    /// Get tracker for a task
    /// </summary>
    Task<Tracker?> GetTrackerAsync(Guid taskId);

    /// <summary>
    /// Force update tracker status (for demo/testing)
    /// </summary>
    Task<Tracker> ForceUpdateTrackerAsync(Guid trackerId);

    /// <summary>
    /// Get all trackers that need updating
    /// </summary>
    Task<List<Tracker>> GetTrackersForUpdateAsync();
}
