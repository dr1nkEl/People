using MediatR;
using People.UseCases.PR.MarkTimeoutReviews;

namespace People.Web.BackgroundJobs;

/// <summary>
/// Background jobs for review.
/// </summary>
public class ReviewBackgroundJobs
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator"><see cref="IMediator"/>.</param>
    public ReviewBackgroundJobs(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// <see cref="MarkTimeoutReviewsCommand"/>.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    public async Task ProcessTimeoutReviewsAsync(CancellationToken cancellationToken)
    {
        await mediator.Send(new MarkTimeoutReviewsCommand(), cancellationToken);
    }
}
