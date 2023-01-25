using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Administration.GetRolesWithPermissions;
using People.UseCases.Branches;
using People.UseCases.Branches.GetBranchNameById;
using People.UseCases.Common.Identity;
using People.UseCases.Positions.Queries.GetPositions;
using People.UseCases.Users.AddReportingUsers;
using People.UseCases.Users.AddUserToPosition;
using People.UseCases.Users.ChangeUserRole;
using People.UseCases.Users.GetUserInfo;
using People.UseCases.Users.GetUsersShortInfo;
using People.Web.ViewModels;
using People.Web.ViewModels.User;

namespace People.Web.Controllers;

/// <summary>
/// User Controller.
/// </summary>
public class UserController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="mapper">Mapper.</param>
    /// <param name="loggedUserAccessor">Logged user accessor.</param>
    public UserController(IMediator mediator, IMapper mapper, ILoggedUserAccessor loggedUserAccessor)
    {
        this.mediator = mediator;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <summary>
    /// GET list view.
    /// </summary>
    /// <returns>View.</returns>
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var branches = await mediator.Send(new GetAllBranchesQuery(), cancellationToken);
        var model = new UserListViewModel()
        {
            Branches = mapper.Map<IEnumerable<BranchViewModel>>(branches).OrderBy(branch=>branch.Name),
        };
        return View(model);
    }

    /// <summary>
    /// GET user info by ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet]
    public async Task<IActionResult> Info(int userId, CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new GetUserInfoByIdQuery(userId), cancellationToken);
        var branch = await mediator.Send(new GetBranchNameByIdQuery(user.BranchId), cancellationToken);
        var allRoles = await mediator.Send(new GetRolesAndClaimsQuery(), cancellationToken);
        var viewModel = new UserInfoViewModel()
        {
            User = user,
            Branch = branch,
            AllRoles = allRoles,
        };

        if (loggedUserAccessor.HasClaim(CustomClaimTypes.Permission, Permissions.Management))
        {
            var positions = await mediator.Send(new GetPositionsQuery(), cancellationToken);
            var users = await mediator.Send(new GetUsersShortInfoQuery(), cancellationToken);
            viewModel.Positions = positions;
            viewModel.Users = users;
        }
        return View(viewModel);
    }

    /// <summary>
    /// POST child users of user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="reportingUserIds">Reporting user IDs.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>LocalRedirect.</returns>
    [Authorize(Policy = Permissions.Management)]
    [HttpPost]
    public async Task<IActionResult> EditReportingUsers(int userId, IEnumerable<int> reportingUserIds, CancellationToken cancellationToken)
    {
        await mediator.Send(new EditReportingUsersCommand(userId, reportingUserIds), cancellationToken);
        return RedirectToAction("Info", new
        {
            userId = userId
        });
    }

    /// <summary>
    /// POST new positions of user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="positionIds">Position IDs.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>LocalRedirect.</returns>
    [Authorize(Policy = Permissions.Management)]
    [HttpPost]
    public async Task<IActionResult> EditPositions([FromQuery]int userId, [FromForm]IEnumerable<int> positionIds, CancellationToken cancellationToken)
    {
        await mediator.Send(new EditUserPositionsCommand(positionIds, userId), cancellationToken);
        return RedirectToAction("Info", new
        {
            userId = userId
        });
    }

    /// <summary>
    /// Change role for some user.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = Permissions.Management)]
    public async Task<IActionResult> ChangeUserRole([Required] ChangeUserRoleCommand changeUserRoleCommand, CancellationToken cancellationToken)
    {
        await mediator.Send(changeUserRoleCommand, cancellationToken);

        return RedirectToAction("Info", new
        {
            userId = changeUserRoleCommand.UserId
        });
    }
}
