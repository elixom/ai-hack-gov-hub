using Microsoft.AspNetCore.SignalR;

namespace GovHub.Api.Hubs;

/// <summary>
/// SignalR hub for real-time chat and task updates
/// </summary>
public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString() ?? "anonymous";
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString() ?? "anonymous";
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Send a message update to a specific user
    /// </summary>
    public async Task SendMessageToUser(string userId, object message)
    {
        await Clients.Group(userId).SendAsync("ReceiveMessage", message);
    }

    /// <summary>
    /// Send a task update to a specific user
    /// </summary>
    public async Task SendTaskUpdate(string userId, object taskUpdate)
    {
        await Clients.Group(userId).SendAsync("TaskUpdated", taskUpdate);
    }

    /// <summary>
    /// Send a tracker update to a specific user
    /// </summary>
    public async Task SendTrackerUpdate(string userId, object trackerUpdate)
    {
        await Clients.Group(userId).SendAsync("TrackerUpdated", trackerUpdate);
    }

    /// <summary>
    /// Send a reminder notification to a specific user
    /// </summary>
    public async Task SendReminderNotification(string userId, object reminder)
    {
        await Clients.Group(userId).SendAsync("ReminderDue", reminder);
    }
}
