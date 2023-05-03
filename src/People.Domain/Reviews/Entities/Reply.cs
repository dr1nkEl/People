using People.Domain.Users.Entities;

namespace People.Domain.Reviews.Entities;

/// <summary>
/// Feedback left for a particular review.
/// </summary>
public class Reply
{
    /// <summary>
    /// Id of the feedback.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the user who has submitted the feedback.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// User who has submitted the feedback.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Date when user has submitted the feedback.
    /// </summary>
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indicates if user has opted out from giving the feedback for a review.
    /// </summary>
    public bool OptOut { get; set; }

    /// <summary>
    /// Answers given in the feedback.
    /// </summary>
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
