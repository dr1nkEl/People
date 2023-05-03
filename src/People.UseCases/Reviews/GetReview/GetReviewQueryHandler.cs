using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.Domain.Exceptions;

namespace People.UseCases.Reviews.GetReview;

/// <summary>
/// Handler for <see cref="GetReviewQuery"/>.
/// </summary>
internal class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, PerformanceReview>
{
    private readonly IAppDbContext appDbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    /// <param name="loggedUserAccessor"><see cref="ILoggedUserAccessor"/>.</param>
    public GetReviewQueryHandler(IAppDbContext appDbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.appDbContext = appDbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc/>
    public async Task<PerformanceReview> Handle(GetReviewQuery request, CancellationToken cancellationToken)
    {
        var curUserId = loggedUserAccessor.GetCurrentUserId();

        var pr = await appDbContext.PerformanceReviews
            .Include(x=>x.FeedbackQuestions)
            .Include(x=>x.ReviewedUserQuestions)
            .Include(x=>x.ReviewedUser)
            .Include(x=>x.ReviewedUserReply)
            .FirstAsync(x => x.Id == request.PrId, cancellationToken);

        if (!(pr.FeedbackUsers.Any(x => x.Id == curUserId)
            || pr.ReviewedUserReplyId == curUserId
            || !pr.Feedback.Any(x=>x.UserId == curUserId)))
        {
            throw new DomainException("Текущий пользователь не участвует в опросе.");
        }

        return pr;
    }
}
