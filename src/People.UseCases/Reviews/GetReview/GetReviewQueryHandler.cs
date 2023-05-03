using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.Reviews.GetReview;

/// <summary>
/// Handler for <see cref="GetReviewQuery"/>.
/// </summary>
internal class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, PerformanceReview>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public GetReviewQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<PerformanceReview> Handle(GetReviewQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.PerformanceReviews
            .Include(x=>x.FeedbackQuestions)
            .Include(x=>x.ReviewedUserQuestions)
            .Include(x=>x.ReviewedUser)
            .Include(x=>x.ReviewedUserReply)
            .FirstAsync(x => x.Id == request.PrId, cancellationToken);
    }
}
