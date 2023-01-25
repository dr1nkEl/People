using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.UseCases.Users;
using People.Web.ViewModels;
using People.Web.ViewModels.User;

namespace People.Web.Controllers.Api;

/// <summary>
/// User API Controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "User")]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="mapper">Mapper.</param>
    public UserController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    /// <summary>
    /// GET Users method.
    /// </summary>
    /// <param name="model">Search model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <response code="400">If modelstate is incorrect.</response>
    /// <response code="200">If everything is OK.</response>
    /// <returns>Users.</returns>
    [HttpGet]
    [Route("GetUsers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<UserDetailedViewModel>>> GetUsers([FromQuery]SearchBranchUsersViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var query = mapper.Map<GetAllUsersDetailedQuery>(model);
        var users = await mediator.Send(query, cancellationToken);
        var mapped = mapper.Map<IEnumerable<UserDetailedViewModel>>(users);
        return Ok(mapped.OrderBy(user=>user.FullName));
    }
}
