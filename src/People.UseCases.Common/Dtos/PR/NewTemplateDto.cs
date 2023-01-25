using People.Domain.Reviews.Entities;

namespace People.UseCases.Common.Dtos.PR;

/// <summary>
/// New perfomance review template DTO.
/// </summary>
public record NewTemplateDto
{
    /// <inheritdoc cref="ReviewTemplate.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="ReviewTemplate.ReviewedUserQuestions"/>
    public IEnumerable<QuestionDto> ReviewedUserQuestions { get; init; }

    /// <inheritdoc cref="ReviewTemplate.FeedbackQuestions"/>
    public IEnumerable<QuestionDto> FeedbackQuestions { get; init; }

    /// <inheritdoc cref="ReviewTemplate.RelatedPositionId"/>
    public int? RelatedPositionId { get; init; }

    /// <inheritdoc cref="ReviewTemplate.ReviewTypeId"/>
    public int ReviewTypeId { get; init; }
}
