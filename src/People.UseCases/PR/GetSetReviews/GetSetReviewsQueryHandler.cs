using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.PR.GetSetReviews;

/// <summary>
/// Handler for <see cref="GetSetReviewsQuery"/>.
/// </summary>
internal class GetSetReviewsQueryHandler : IRequestHandler<GetSetReviewsQuery, IEnumerable<PerformanceReview>>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public GetSetReviewsQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PerformanceReview>> Handle(GetSetReviewsQuery request, CancellationToken cancellationToken)
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
            .Where(x => x.Deadline >= DateOnly.FromDateTime(DateTime.Today) || x.Deadline == null)
            .ToList();
    }
}
