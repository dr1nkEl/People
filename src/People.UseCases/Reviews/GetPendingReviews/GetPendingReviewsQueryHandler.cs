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
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    /// <param name="loggedUserAccessor"><see cref="ILoggedUserAccessor"/>.</param>
    public GetPendingReviewsQueryHandler(IAppDbContext appDbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.appDbContext = appDbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc/>
    public async Task<(IEnumerable<PerformanceReview> ReviewsForUser, IEnumerable<PerformanceReview> UserReviews)> Handle(GetPendingReviewsQuery request, CancellationToken cancellationToken)
    {
        var pendingPrs = (await appDbContext.PerformanceReviews
            .Include(x => x.FeedbackUsers)
            .Include(x=>x.ReviewedUser)
            .Include(x=>x.Feedback)
            .ToListAsync(cancellationToken))
            .Where(x => x.Deadline >= DateOnly.FromDateTime(DateTime.Today) || x.Deadline == null)
            .Where(x => x.CompletedDate == null)
            .ToList();

        var userReviews = pendingPrs.Where(x => x.ReviewedUserId == request.UserId && x.ReviewedUserReplyId == null).ToList();

        var reviewsForUser = pendingPrs.Where(x=>!x.Feedback.Any(x=>x.UserId ==request.UserId) && x.FeedbackUsers.Any(x=>x.Id == request.UserId)).ToList();

        return (reviewsForUser, userReviews);
    }
}
