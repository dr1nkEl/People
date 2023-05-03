using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.PR.GetFinishedReviews;

/// <summary>
/// Handler for <see cref="GetFinishedReviewsQuery"/>.
/// </summary>
internal class GetFinishedReviewsQueryHandler : IRequestHandler<GetFinishedReviewsQuery, IEnumerable<PerformanceReview>>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public GetFinishedReviewsQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PerformanceReview>> Handle(GetFinishedReviewsQuery request, CancellationToken cancellationToken)
    {
        return (await appDbContext
            .PerformanceReviews
            .Include(x => x.Feedback)
            .Include(x => x.ReviewedUserReply)
            .Include(x => x.ReviewedUser)
            .Include(x => x.FeedbackQuestions)
            .Include(x => x.ReviewedUserQuestions)
            .Include(x => x.FeedbackUsers)
            .ToListAsync(cancellationToken))
            .Where(x => x.CompletedDate != null)
            .ToList();
    }
}
