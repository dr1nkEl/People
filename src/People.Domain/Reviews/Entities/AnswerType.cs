namespace People.Domain.Reviews.Entities;

/// <summary>
/// Type of expected answer.
/// </summary>
public enum AnswerType
{
    /// <summary>
    /// Plain multiline text answer.
    /// </summary>
    Text = 0,

    /// <summary>
    /// Select one of several options, and optionally add any comments.
    /// </summary>
    Options = 1,
}
