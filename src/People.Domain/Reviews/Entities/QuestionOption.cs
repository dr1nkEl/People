namespace People.Domain.Reviews.Entities;

/// <summary>
/// Available option for a question.
/// </summary>
public class QuestionOption
{
    /// <summary>
    /// Id of the option.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Option text.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Order to sort options by.
    /// </summary>
    public int Order { get; set; }
}
