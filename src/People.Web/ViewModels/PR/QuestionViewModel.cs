using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.PR;

/// <summary>
/// Question view model.
/// </summary>
public record QuestionViewModel
{
    /// <inheritdoc cref="Question.AnswerType"/>
    public int AnswerType { get; init; }

    /// <inheritdoc cref="Question.Title"/>
    public string Title { get; init; }

    /// <inheritdoc cref="Question.Options"/>
    public ICollection<QuestionOptionViewModel> Options { get; init; }
}
