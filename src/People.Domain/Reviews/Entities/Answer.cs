namespace People.Domain.Reviews.Entities;

/// <summary>
/// Contains information about an answer for a question.
/// </summary>
public class Answer
{
    /// <summary>
    /// Id of the answer.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the associated question.
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Associated question.
    /// </summary>
    public Question Question { get; set; }

    /// <summary>
    /// Answer text or comments.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Selected option id if associated question has options to choose from.
    /// </summary>
    public int? OptionId { get; set; }

    /// <summary>
    /// Indicates if no answer has been given to the question.
    /// This can be possible if person is not able for any reason to answer a question.
    /// In that case, <see cref="Text"/> and <see cref="OptionId"/> will be empty.
    /// </summary>
    public bool NoAnswer { get; set; }

    /// <summary>
    /// Id of the associated feedback entity.
    /// </summary>
    public int FeedbackId { get; set; }

    /// <summary>
    /// Associated feedback data.
    /// </summary>
    public Reply Feedback { get; set; }
}
