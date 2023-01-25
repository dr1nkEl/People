using People.Domain.Users.Entities;

namespace People.Domain.Reviews.Entities;

/// <summary>
/// Template for a performance review.
/// </summary>
public class ReviewTemplate
{
    /// <summary>
    /// Id of the template.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the template.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Questions that will be given to reviewed user.
    /// </summary>
    public ICollection<Question> ReviewedUserQuestions { get; set; } = new List<Question>();

    /// <summary>
    /// Questions that will be given to users who leave feedback for the reviewed user.
    /// </summary>
    public ICollection<Question> FeedbackQuestions { get; set; } = new List<Question>();

    /// <summary>
    /// Id of the position that is associated with this template.
    /// </summary>
    public int? RelatedPositionId { get; set; }

    /// <summary>
    /// Position associated with this template.
    /// </summary>
    public Position RelatedPosition { get; set; }

    /// <summary>
    /// Id of the review type.
    /// </summary>
    public int ReviewTypeId { get; set; }

    /// <summary>
    /// Type of review.
    /// </summary>
    public ReviewType ReviewType { get; set; }

    /// <summary>
    /// Deleted at.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}
