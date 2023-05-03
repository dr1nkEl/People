using MediatR;
using People.Domain.Reviews.Entities;

namespace People.UseCases.Reviews.GetPendingReviews;

/// <summary>
/// Get pending reviews query.
/// </summary>
/// <param name="UserId">Id of user.</param>
public record GetPendingReviewsQuery(int UserId) : IRequest<(IEnumerable<PerformanceReview> ReviewsForUser, IEnumerable<PerformanceReview> UserReviews)>;
