using People.Domain.Reviews.Entities;

namespace People.UseCases.Common.Dtos.PR;

/// <summary>
/// Perfomance review template detailed DTO.
/// </summary>
public record PRTemplateDetailedDto
{
    /// <inheritdoc cref="ReviewTemplate.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="ReviewTemplate.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="ReviewTemplate.ReviewedUserQuestions"/>
    public ICollection<QuestionDto> ReviewedUserQuestions { get; init; }

    /// <inheritdoc cref="ReviewTemplate.FeedbackQuestions"/>
    public ICollection<QuestionDto> FeedbackQuestions { get; init; }

    /// <inheritdoc cref="ReviewTemplate.RelatedPosition"/>
    public int? RelatedPositionId { get; init; }

    /// <inheritdoc cref="ReviewTemplate.ReviewType"/>
    public int ReviewTypeId { get; init; }
}
