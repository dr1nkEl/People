using People.Domain.Reviews.Entities;

namespace People.UseCases.Common.Dtos.PR;

/// <summary>
/// Question option DTO.
/// </summary>
public record QuestionOptionDto
{
    /// <inheritdoc cref="QuestionOption.Title"/>
    public string Title { get; init; }

    /// <inheritdoc cref="QuestionOption.Order"/>
    public int Order { get; init; }
}
