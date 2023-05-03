using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.PR.MarkTimeoutReviews;

/// <summary>
/// Handler for <see cref="MarkTimeoutReviews"/>.
/// </summary>
internal class MarkTimeoutReviewsCommandHandler : AsyncRequestHandler<MarkTimeoutReviewsCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public MarkTimeoutReviewsCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(MarkTimeoutReviewsCommand request, CancellationToken cancellationToken)
    {
        var reviewsToMark = await appDbContext
            .PerformanceReviews
            .Where(x => x.CompletedDate == null && x.Deadline < DateOnly.FromDateTime(DateTime.Today))
            .ToListAsync(cancellationToken);

        foreach (var review in reviewsToMark)
        {
            review.CompletedDate = DateTime.Today;
            review.IsFinishedByTimeout = true;
        }

        appDbContext.PerformanceReviews.UpdateRange(reviewsToMark);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
