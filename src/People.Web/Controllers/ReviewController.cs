using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Reviews.GetPendingReviews;
using People.Web.ViewModels.Review;

namespace People.Web.Controllers;

/// <summary>
/// Review controller.
/// </summary>
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
}
