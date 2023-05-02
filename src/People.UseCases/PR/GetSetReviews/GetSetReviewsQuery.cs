using MediatR;
using People.Domain.Reviews.Entities;

namespace People.UseCases.PR.GetSetReviews;

/// <summary>
/// Get set reviews query.
/// </summary>
public record GetSetReviewsQuery() : IRequest<IEnumerable<PerformanceReview>>;
