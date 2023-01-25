namespace People.Domain.Reviews.Entities;

/// <summary>
/// Reminds about a performance review.
/// </summary>
public class ReviewReminder
{
    /// <summary>
    /// Id of the reminder.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Period in months after which the reminder will be triggered.
    /// </summary>
    public int MonthCount { get; set; }

    /// <summary>
    /// Id of the review that was created from that reminder.
    /// </summary>
    public int ReviewId { get; set; }

    /// <summary>
    /// Review that was created from that reminder.
    /// </summary>
    public PerformanceReview Review { get; set; }

    /// <summary>
    /// When the last time the reminder was triggered.
    /// </summary>
    public DateOnly? LastTriggeredDay { get; set; }
}
