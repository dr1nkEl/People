using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Reviews.GetPendingReviews;
using People.UseCases.Reviews.GetReview;
using People.UseCases.Reviews.LeaveReview;
using People.Web.ViewModels.Review;

namespace People.Web.Controllers;

/// <summary>
/// Review controller.
/// </summary>
[Route("[controller]/[action]")]
public class ReviewController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator"><see cref="IMediator"/>.</param>
    /// <param name="mapper"><see cref="IMapper"/>.</param>
    /// <param name="loggedUserAccessor"><see cref="ILoggedUserAccessor"/>.</param>
    public ReviewController(IMediator mediator, IMapper mapper, ILoggedUserAccessor loggedUserAccessor)
    {
        this.mediator = mediator;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <summary>
    /// GET pending reviews.
    /// </summary>
    public async Task<IActionResult> Pending(CancellationToken cancellationToken)
    {
        var res = await mediator.Send(new GetPendingReviewsQuery(loggedUserAccessor.GetCurrentUserId()), cancellationToken);
        return View(new PendingViewModel() { ReviewsForUser = res.ReviewsForUser, UserReviews = res.UserReviews });
    }

    /// <summary>
    /// GET pr processing page.
    /// </summary>
    /// <param name="prId">Id of set perfomance review.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    [HttpGet("{prId}")]
    public async Task<IActionResult> Process(int prId, CancellationToken cancellationToken)
    {
        var review = await mediator.Send(new GetReviewQuery(prId), cancellationToken);

        var isForCurrent = loggedUserAccessor.GetCurrentUserId() == review.ReviewedUserId;
        return View(new ProcessViewModel() { IsPrForCurrentUser = isForCurrent, Review = review });
    }

    /// <summary>
    /// POST leave reply for pr.
    /// </summary>
    /// <param name="model"><see cref="ProcessViewModel"/>.</param>
    /// <param name="prId">Id of pr.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    [HttpPost("{prId}")]
    public async Task<IActionResult> Process([FromRoute]int prId, [FromForm]ProcessViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await mediator.Send(new LeaveReviewCommand(model.Reply, prId), cancellationToken);
        return RedirectToAction("Pending");
    }
}
