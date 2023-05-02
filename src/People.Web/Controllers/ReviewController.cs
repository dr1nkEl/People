using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace People.Web.Controllers;

/// <summary>
/// Review controller.
/// </summary>
public class ReviewController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator"><see cref="IMediator"/>.</param>
    /// <param name="mapper"><see cref="IMapper"/>.</param>
    public ReviewController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public IActionResult List()
    {
        return View();
    }
}
