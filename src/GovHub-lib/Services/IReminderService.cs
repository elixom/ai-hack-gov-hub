using GovHub.Lib.Models;

namespace GovHub.Lib.Services;

/// <summary>
/// Service for managing task reminders
/// </summary>
public interface IReminderService
{
    /// <summary>
    /// Add a reminder for a task
    /// </summary>
    Task<Reminder> AddReminderAsync(Guid taskId, string message, DateTime dueDate);

    /// <summary>
    /// Add a reminder with time offset (e.g., "1h", "1d", "7d")
    /// </summary>
    Task<Reminder> AddReminderWithOffsetAsync(Guid taskId, string message, string offset, DateTime? referenceDate = null);

    /// <summary>
    /// Get all reminders for a task
    /// </summary>
    Task<List<Reminder>> GetRemindersAsync(Guid taskId);

    /// <summary>
    /// Get due reminders that haven't been dismissed
    /// </summary>
    Task<List<Reminder>> GetDueRemindersAsync();

    /// <summary>
    /// Dismiss a reminder
    /// </summary>
    Task DismissReminderAsync(Guid reminderId);
}
