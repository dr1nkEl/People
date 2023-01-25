namespace People.Domain.Reviews.Entities;

/// <summary>
/// Contains information about a question.
/// </summary>
public class Question
{
    /// <summary>
    /// Id of the question.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Type of expected answer to the question.
    /// </summary>
    public AnswerType AnswerType { get; set; }

    /// <summary>
    /// Title of the question / question itself.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Order to sort questions within a single review/template.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Options available for the specified question.
    /// </summary>
    public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();

    /// <summary>
    /// Template in which this question is used for as question for user feedback.
    /// </summary>
    public ReviewTemplate UserReviewTemplate { get; set; }

    /// <summary>
    /// Template in which this question is used for regular feedback.
    /// </summary>
    public ReviewTemplate FeedbackTemplate { get; set; }

    /// <summary>
    /// Performance review that uses this question for user feedback.
    /// </summary>
    public PerformanceReview UserReview { get; set; }

    /// <summary>
    /// Performance review that uses this question for regular feedback.
    /// </summary>
    public PerformanceReview FeedbackReview { get; set; }
}
