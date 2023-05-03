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
        var pr = await appDbContext.PerformanceReviews
            .Include(x=>x.Feedback)
            .Include(x=>x.ReviewedUserReply)
            .FirstAsync(x => x.Id == request.PrId, cancellationToken);

        request.UserReply.UserId = loggedUserAccessor.GetCurrentUserId();

        if (pr.ReviewedUserId == loggedUserAccessor.GetCurrentUserId())
        {
            pr.ReviewedUserReply = request.UserReply;
        }
        else
        {
            pr.Feedback.Add(request.UserReply);
        }
        appDbContext.PerformanceReviews.Update(pr);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
