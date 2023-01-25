using People.Domain.Users.Entities;

namespace People.Domain.Reviews.Entities;

/// <summary>
/// Review of performance of particular person.
/// </summary>
public class PerformanceReview
{
    /// <summary>
    /// Id of the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// When the review was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Id of the user who is being reviewed.
    /// </summary>
    public int ReviewedUserId { get; set; }

    /// <summary>
    /// User who is being reviewed.
    /// </summary>
    public User ReviewedUser { get; set; }

    /// <summary>
    /// Questions that will be given to reviewed user.
    /// </summary>
    public ICollection<Question> ReviewedUserQuestions { get; set; } = new List<Question>();

    /// <summary>
    /// Questions that will be given to users who leave feedback for the reviewed user.
    /// </summary>
    public ICollection<Question> FeedbackQuestions { get; set; } = new List<Question>();

    /// <summary>
    /// List of users who should leave feedback for the reviewed user.
    /// </summary>
    public ICollection<User> FeedbackUsers { get; set; } = new List<User>();

    /// <summary>
    /// Id of the user who has created the PR.
    /// </summary>
    public int CreatedById { get; set; }

    /// <summary>
    /// User who has initiated the PR.
    /// </summary>
    public User CreatedBy { get; set; }

    /// <summary>
    /// Feedback people have left for the reviewed user.
    /// </summary>
    public ICollection<Reply> Feedback { get; set; } = new List<Reply>();

    /// <summary>
    /// Id of the reply left by reviewed user.
    /// </summary>
    public int? ReviewedUserReplyId { get; set; }

    /// <summary>
    /// Reply left by reviewed user.
    /// </summary>
    public Reply ReviewedUserReply { get; set; }

    /// <summary>
    /// Id of the reply left by the administrator during the final review.
    /// </summary>
    public int? FinalReplyId { get; set; }

    /// <summary>
    /// Reply left by the administrator during the final review.
    /// </summary>
    public Reply FinalReply { get; set; }

    /// <summary>
    /// Optional deadline for submitting the review feedback.
    /// </summary>
    public DateOnly? Deadline { get; set; }

    /// <summary>
    /// Indicates when the final review has been submitted.
    /// If this has value, it means that review is completed and locked out for editing.
    /// </summary>
    public DateTime? CompletedDate { get; set; }
}
