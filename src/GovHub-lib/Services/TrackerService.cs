using GovHub.Lib.Enums;
using GovHub.Lib.Models;

namespace GovHub.Lib.Services;

/// <summary>
/// Mock implementation of tracker service with simulated status updates
/// </summary>
public class TrackerService : ITrackerService
{
    private readonly Dictionary<Guid, Tracker> _trackers = new();
    private readonly Random _random = new();

    public Task<Tracker> AttachTrackerAsync(Guid taskId, string provider, string referenceNumber)
    {
        var tracker = new Tracker
        {
            TaskId = taskId,
            Provider = provider,
            Reference = referenceNumber,
            Status = TrackerStatus.Submitted,
            StatusMessage = "Application submitted successfully"
        };

        _trackers[tracker.Id] = tracker;
        return Task.FromResult(tracker);
    }

    public Task<Tracker?> GetTrackerAsync(Guid taskId)
    {
        var tracker = _trackers.Values.FirstOrDefault(t => t.TaskId == taskId);
        return Task.FromResult(tracker);
    }

    public Task<Tracker> ForceUpdateTrackerAsync(Guid trackerId)
    {
        if (!_trackers.TryGetValue(trackerId, out var tracker))
        {
            throw new KeyNotFoundException($"Tracker {trackerId} not found");
        }

        // Simulate status progression based on provider
        var newStatus = SimulateStatusTransition(tracker.Provider, tracker.Status);
        var newMessage = GetStatusMessage(tracker.Provider, newStatus);

        var updatedTracker = tracker with
        {
            Status = newStatus,
            StatusMessage = newMessage,
            LastUpdated = DateTime.UtcNow
        };

        _trackers[trackerId] = updatedTracker;
        return Task.FromResult(updatedTracker);
    }

    public Task<List<Tracker>> GetTrackersForUpdateAsync()
    {
        // Return trackers that haven't reached terminal status
        var activeTrackers = _trackers.Values
            .Where(t => t.Status != TrackerStatus.Completed && t.Status != TrackerStatus.Rejected)
            .ToList();

        return Task.FromResult(activeTrackers);
    }

    private TrackerStatus SimulateStatusTransition(string provider, TrackerStatus currentStatus)
    {
        // Different providers have different workflows
        return provider.ToUpperInvariant() switch
        {
            "MOCK_FITNESS" => currentStatus switch
            {
                TrackerStatus.NotStarted => TrackerStatus.Submitted,
                TrackerStatus.Submitted => TrackerStatus.UnderReview,
                TrackerStatus.UnderReview => TrackerStatus.AwaitingPayment,
                TrackerStatus.AwaitingPayment => TrackerStatus.Processing,
                TrackerStatus.Processing => TrackerStatus.ReadyForPickup,
                TrackerStatus.ReadyForPickup => TrackerStatus.Completed,
                _ => currentStatus
            },
            "MOCK_NHT" => currentStatus switch
            {
                TrackerStatus.NotStarted => TrackerStatus.Submitted,
                TrackerStatus.Submitted => TrackerStatus.UnderReview,
                TrackerStatus.UnderReview => _random.Next(0, 10) < 8 ? TrackerStatus.Processing : TrackerStatus.Rejected,
                TrackerStatus.Processing => TrackerStatus.Completed,
                _ => currentStatus
            },
            "MOCK_TAX" => currentStatus switch
            {
                TrackerStatus.NotStarted => TrackerStatus.Submitted,
                TrackerStatus.Submitted => TrackerStatus.Processing,
                TrackerStatus.Processing => TrackerStatus.Completed,
                _ => currentStatus
            },
            _ => currentStatus
        };
    }

    private static string GetStatusMessage(string provider, TrackerStatus status)
    {
        return (provider.ToUpperInvariant(), status) switch
        {
            ("MOCK_FITNESS", TrackerStatus.Submitted) => "Vehicle fitness application received",
            ("MOCK_FITNESS", TrackerStatus.UnderReview) => "Inspector reviewing vehicle documents",
            ("MOCK_FITNESS", TrackerStatus.AwaitingPayment) => "Payment required to proceed - JMD $3,500",
            ("MOCK_FITNESS", TrackerStatus.Processing) => "Processing certificate issuance",
            ("MOCK_FITNESS", TrackerStatus.ReadyForPickup) => "Certificate ready at Island Traffic Authority office",
            ("MOCK_FITNESS", TrackerStatus.Completed) => "Fitness certificate issued successfully",

            ("MOCK_NHT", TrackerStatus.Submitted) => "NHT benefits application received",
            ("MOCK_NHT", TrackerStatus.UnderReview) => "Verifying employment and contribution history",
            ("MOCK_NHT", TrackerStatus.Processing) => "Loan application approved - preparing documentation",
            ("MOCK_NHT", TrackerStatus.Completed) => "Benefits disbursed to registered account",
            ("MOCK_NHT", TrackerStatus.Rejected) => "Application denied - insufficient contribution period",

            ("MOCK_TAX", TrackerStatus.Submitted) => "Tax return filed successfully",
            ("MOCK_TAX", TrackerStatus.Processing) => "TAJ processing your tax assessment",
            ("MOCK_TAX", TrackerStatus.Completed) => "Tax return processed - refund of JMD $12,450",

            _ => $"Status: {status}"
        };
    }
}
