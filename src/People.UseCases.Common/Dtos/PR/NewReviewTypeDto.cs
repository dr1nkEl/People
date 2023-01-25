using People.Domain.Reviews.Entities;

namespace People.UseCases.Common.Dtos.PR;

/// <summary>
/// New review type DTO.
/// </summary>
public record NewReviewTypeDto
{
    /// <inheritdoc cref="ReviewType.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="ReviewType.Interval"/>
    public Interval? Interval { get; init; }

    /// <inheritdoc cref="ReviewType.IntervalAmount"/>
    public int? IntervalAmount { get; init; }
}
