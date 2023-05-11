using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Administration.Attributes.GetUserAttributes;
using People.UseCases.Administration.GetRolesOfUser;
using People.UseCases.Administration.GetRolesWithPermissions;
using People.UseCases.Attributes.GetUserAttributes;
using People.UseCases.Branches;
using People.UseCases.Branches.GetBranchNameById;
using People.UseCases.Common.Identity;
using People.UseCases.Positions.Queries.GetPositions;
using People.UseCases.Reviews.GetPendingReviews;
using People.UseCases.Users.AddReportingUsers;
using People.UseCases.Users.AddUserToPosition;
using People.UseCases.Users.ChangePasswordCommand;
using People.UseCases.Users.ChangeUserRole;
using People.UseCases.Users.GetUserInfo;
using People.UseCases.Users.GetUsersShortInfo;
using People.UseCases.Users.SetActiveAttributes;
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
        var pendingReviews = await mediator.Send(new GetPendingReviewsQuery(userId), cancellationToken);

        var attributes = await mediator.Send(new GetUserAttributesByIdQuery(userId), cancellationToken);
        var userRoles = await mediator.Send(new GetRolesOfUserQuery(loggedUserAccessor.GetCurrentUserId()), cancellationToken);
        var allAttributes = mapper.Map<IEnumerable<UserAttribute>>(await mediator.Send(new GetUserAttributesQuery(false), cancellationToken));

        var viewModel = new UserInfoViewModel()
        {
            User = user,
            Branch = branch,
            AllRoles = allRoles,
            UserReviews = pendingReviews.UserReviews,
            ReviewsForUser = pendingReviews.ReviewsForUser,
            AttributesOfUser = attributes,
            RolesOfUser = userRoles,
            AllAttributes = allAttributes,
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
    /// POST redirect to current user profile.
    /// </summary>
    [HttpPost]
    public IActionResult MyProfile() => RedirectToAction("Info", new
    {
        userId = loggedUserAccessor.GetCurrentUserId(),
    });

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

    /// <summary>
    /// GET change password view.
    /// </summary>
    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    /// <summary>
    /// Method to redirect for change password page.
    /// </summary>
    [HttpPost]
    public IActionResult ChangePasswordRedirect() => RedirectToAction("ChangePassword");

    /// <summary>
    /// Changes password of user.
    /// </summary>
    /// <param name="model"><see cref="ChangePasswordViewModel"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, CancellationToken cancellationToken)
    {
        await mediator.Send(new ChangePasswordCommand(loggedUserAccessor.GetCurrentUserId(), model.OldPassword, model.NewPassword), cancellationToken);
        return RedirectToAction("Info", new
        {
            userId = loggedUserAccessor.GetCurrentUserId(),
        });
    }

    /// <summary>
    /// Sets active attributes for user.
    /// </summary>
    /// <param name="model"><see cref="UserInfoViewModel"/>.</param>
    [HttpPost]
    public async Task<IActionResult> SetAttributesForUser(UserInfoViewModel model)
    {
        await mediator.Send(new SetActiveAttributesCommand(model.User.Id, model.AttributeIdsToSet));

        return RedirectToAction("Info", new
        {
            userId = model.User.Id,
        });
    }
}
