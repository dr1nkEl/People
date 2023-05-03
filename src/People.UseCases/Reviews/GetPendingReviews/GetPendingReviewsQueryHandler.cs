using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.Reviews.GetPendingReviews;

/// <summary>
/// Handler for <see cref="GetPendingReviewsQuery"/>.
/// </summary>
internal class GetPendingReviewsQueryHandler : IRequestHandler<GetPendingReviewsQuery, (IEnumerable<PerformanceReview> ReviewsForUser, IEnumerable<PerformanceReview> UserReviews)>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public GetPendingReviewsQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<(IEnumerable<PerformanceReview> ReviewsForUser, IEnumerable<PerformanceReview> UserReviews)> Handle(GetPendingReviewsQuery request, CancellationToken cancellationToken)
    {
        var pendingPrs = (await appDbContext.PerformanceReviews.Include(x => x.FeedbackUsers).Include(x=>x.ReviewedUser)
            .ToListAsync(cancellationToken))
            .Where(x => x.Deadline >= DateOnly.FromDateTime(DateTime.Today) || x.Deadline == null)
            .ToList();

        var userReviews = pendingPrs.Where(x => x.ReviewedUserId == request.UserId).ToList();

        var reviewsForUser = pendingPrs.Where(x=>x.FeedbackUsers.Any(x=>x.Id == request.UserId)).ToList();

        return (userReviews, reviewsForUser);
    }
}
