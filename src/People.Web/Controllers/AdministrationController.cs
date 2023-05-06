using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.Domain.Users.Entities;
using People.UseCases.Administration.Attributes.GetUserAttributes;
using People.UseCases.Administration.GetRolesWithPermissions;
using People.UseCases.Administration.UpdatePermissionsForRole;
using People.UseCases.Branches;
using People.UseCases.Common.Identity;
using People.UseCases.PR.GetTemplates;
using People.UseCases.Users.CreateUser;
using People.Web.ViewModels;
using People.Web.ViewModels.Attribute;
using People.Web.ViewModels.PR;
using People.Web.ViewModels.User;

namespace People.Web.Controllers;

/// <summary>
/// Admin controller for administration page.
/// </summary>
[Route("[controller]")]
[Authorize(Policy = Permissions.Management)]
public class AdministrationController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AdministrationController(IMediator mediator, IMapper mapper)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    /// <summary>
    /// Get administration page.
    /// </summary>
    [HttpGet("[action]")]
    public async Task<IActionResult> RoleListAdministration(CancellationToken cancellationToken)
    {
        var roles = await mediator.Send(new GetRolesAndClaimsQuery(), cancellationToken);
        var model = new RoleListViewModel()
        {
            Roles = roles,
        };
        return View(model);
    }

    /// <summary>
    /// GET Attributes view.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> Attributes(CancellationToken cancellationToken)
    {
        var attributesDto = await mediator.Send(new GetUserAttributesQuery(), cancellationToken);
        var attributes = mapper.Map<IEnumerable<AttributeViewModel>>(attributesDto);
        var model = new AttributeListViewModel
        {
            Attributes = attributes,
        };
        return View(model);
    }

    /// <summary>
    /// GET List view.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> PRTemplates(CancellationToken cancellationToken)
    {
        var prTemplatesDto = await mediator.Send(new GetPRTemplatesQuery(), cancellationToken);
        var prTemplates = mapper.Map<IEnumerable<TemplateViewModel>>(prTemplatesDto);

        var model = new ListViewModel
        {
            Templates = prTemplates,
        };
        return View(model);
    }

    /// <summary>
    /// Update claims for roles.
    /// </summary>
    /// <param name="command">Update claims for role command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("[action]")]
    public async Task UpdateClaimsForRoles(UpdatePermissionsForRoleCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// GET page of user creation.
    /// </summary>
    [HttpGet("[action]")]
    public async Task<IActionResult> CreateUser(CancellationToken cancellationToken)
    {
        var branches = await mediator.Send(new GetAllBranchesQuery(), cancellationToken);

        var model = new CreateUserViewModel
        {
            Branches = mapper.Map<IEnumerable<Branch>>(branches),
        };
        return View(model);
    }

    /// <summary>
    /// POST create new user.
    /// </summary>
    /// <param name="model"><see cref="CreateUserViewModel"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateUser(CreateUserViewModel model, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(new CreateUserCommand(model.Email, model.FirstName, model.LastName, DateOnly.FromDateTime(model.BirthDate), model.Password, model.BranchId), cancellationToken);
        return RedirectToAction("Info", "User", new { userId = id });
    }
}
