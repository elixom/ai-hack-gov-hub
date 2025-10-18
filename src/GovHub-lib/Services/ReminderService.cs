using GovHub.Lib.Models;

namespace GovHub.Lib.Services;

/// <summary>
/// In-memory implementation of reminder service
/// </summary>
public class ReminderService : IReminderService
{
    private readonly Dictionary<Guid, Reminder> _reminders = new();

    public Task<Reminder> AddReminderAsync(Guid taskId, string message, DateTime dueDate)
    {
        var reminder = new Reminder
        {
            TaskId = taskId,
            Message = message,
            DueDate = dueDate
        };

        _reminders[reminder.Id] = reminder;
        return Task.FromResult(reminder);
    }

    public Task<Reminder> AddReminderWithOffsetAsync(Guid taskId, string message, string offset, DateTime? referenceDate = null)
    {
        var reference = referenceDate ?? DateTime.UtcNow;
        var dueDate = ParseOffset(offset, reference);

        return AddReminderAsync(taskId, message, dueDate);
    }

    public Task<List<Reminder>> GetRemindersAsync(Guid taskId)
    {
        var reminders = _reminders.Values
            .Where(r => r.TaskId == taskId)
            .OrderBy(r => r.DueDate)
            .ToList();

        return Task.FromResult(reminders);
    }

    public Task<List<Reminder>> GetDueRemindersAsync()
    {
        var now = DateTime.UtcNow;
        var dueReminders = _reminders.Values
            .Where(r => !r.IsDismissed && r.DueDate <= now)
            .OrderBy(r => r.DueDate)
            .ToList();

        return Task.FromResult(dueReminders);
    }

    public Task DismissReminderAsync(Guid reminderId)
    {
        if (_reminders.TryGetValue(reminderId, out var reminder))
        {
            var updated = reminder with
            {
                IsDismissed = true,
                DismissedAt = DateTime.UtcNow
            };
            _reminders[reminderId] = updated;
        }

        return Task.CompletedTask;
    }

    private static DateTime ParseOffset(string offset, DateTime reference)
    {
        // Parse time offset strings like "1h", "1d", "7d", "1m"
        offset = offset.Trim().ToLowerInvariant();

        if (offset.EndsWith('m'))
        {
            if (int.TryParse(offset[..^1], out var minutes))
                return reference.AddMinutes(minutes);
        }
        else if (offset.EndsWith('h'))
        {
            if (int.TryParse(offset[..^1], out var hours))
                return reference.AddHours(hours);
        }
        else if (offset.EndsWith('d'))
        {
            if (int.TryParse(offset[..^1], out var days))
                return reference.AddDays(days);
        }

        // Default to 1 day
        return reference.AddDays(1);
    }
}
