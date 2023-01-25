using People.Domain.Reviews.Entities;

namespace People.UseCases.Common.Dtos.PR;

/// <summary>
/// Question DTO.
/// </summary>
public record QuestionDto
{
    /// <inheritdoc cref="Question.AnswerType"/>
    public AnswerType AnswerType { get; init; }

    /// <inheritdoc cref="Question.Title"/>
    public string Title { get; init; }

    /// <inheritdoc cref="Question.Order"/>
    public int Order { get; init; }

    /// <inheritdoc cref="Question.Options"/>
    public ICollection<QuestionOptionDto> Options { get; init; }
}
