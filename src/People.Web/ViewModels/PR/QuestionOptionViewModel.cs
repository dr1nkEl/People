using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.PR;

/// <summary>
/// Question option view model.
/// </summary>
public record QuestionOptionViewModel
{
    /// <inheritdoc cref="QuestionOption.Title"/>
    public string Title { get; init; }

    /// <inheritdoc cref="QuestionOption.Order"/>
    public int Order { get; init; }
}
