using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.Reviews.LeaveReview;

/// <summary>
/// Handler for <see cref="LeaveReviewCommand"/>.
/// </summary>
internal class LeaveReviewCommandHandler : AsyncRequestHandler<LeaveReviewCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    /// <param name="loggedUserAccessor"><see cref="ILoggedUserAccessor"/>.</param>
    public LeaveReviewCommandHandler(IAppDbContext appDbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.appDbContext = appDbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc/>.
    protected override async Task Handle(LeaveReviewCommand request, CancellationToken cancellationToken)
    {
        var curUserId = loggedUserAccessor.GetCurrentUserId();

        var pr = await appDbContext.PerformanceReviews
            .Include(x=>x.Feedback)
            .Include(x=>x.FeedbackUsers)
            .Include(x=>x.ReviewedUserReply)
            .FirstAsync(x => x.Id == request.PrId, cancellationToken);

        if (!(pr.FeedbackUsers.Any(x=>x.Id == curUserId) || pr.ReviewedUserReplyId == curUserId || !pr.Feedback.Any(x => x.UserId == curUserId)))
        {
            throw new Exception("Текущий пользователь не участвует в этом опросе.");
        }

        request.UserReply.UserId = loggedUserAccessor.GetCurrentUserId();

        if (pr.ReviewedUserId == loggedUserAccessor.GetCurrentUserId())
        {
            pr.ReviewedUserReply = request.UserReply;
        }
        else
        {
            pr.Feedback.Add(request.UserReply);
        }

        if (pr.ReviewedUserReply != null && pr.FeedbackUsers.Count == pr.Feedback.Count)
        {
            pr.CompletedDate = DateTime.UtcNow;
            pr.IsFinishedByTimeout = false;
        }

        appDbContext.PerformanceReviews.Update(pr);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
