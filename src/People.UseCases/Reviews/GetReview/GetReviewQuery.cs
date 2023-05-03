using MediatR;
using People.Domain.Reviews.Entities;

namespace People.UseCases.Reviews.GetReview;

/// <summary>
/// Get perfomance review query.
/// </summary>
/// <param name="PrId">Id of PR.</param>
public record GetReviewQuery(int PrId) : IRequest<PerformanceReview>;
