using MediatR;
using People.Domain.Reviews.Entities;

namespace People.UseCases.PR.GetFinishedReviews;

/// <summary>
/// Get finished reviews query.
/// </summary>
public record GetFinishedReviewsQuery() : IRequest<IEnumerable<PerformanceReview>>;
