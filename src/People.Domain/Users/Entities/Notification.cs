namespace People.Domain.Users.Entities;

/// <summary>
/// Notification to user about any event.
/// </summary>
public class Notification
{
    /// <summary>
    /// Id of the notification.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Notification text.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Id of the target notification user.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Target user for notification.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Any data associated with the notification.
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// Type of the notification.
    /// </summary>
    public NotificationType NotificationType { get; set; }
}
