using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.UseCases.Administration.Attributes.GetUserAttribute;
using People.UseCases.Administration.GetRolesWithPermissions;
using People.Web.ViewModels.Attribute;
using People.UseCases.Administration.Attributes.CreateUserAttribute;
using People.UseCases.Common.Dtos.Attribute;
using People.UseCases.Administration.Attributes.DeleteUserAttribute;
using People.UseCases.Administration.Attributes.UpdateUserAttribute;

namespace People.Web.Controllers;

/// <inheritdoc/>
[Route("admin/[controller]")]
public class AttributeController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AttributeController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    /// <summary>
    /// GET Index view.
    /// </summary>
    /// <param name="attributeId">Attribute ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet("[action]/{attributeId}")]
    public async Task<IActionResult> Edit(int attributeId, CancellationToken cancellationToken)
    {
        var roles = await mediator.Send(new GetRolesAndClaimsQuery(), cancellationToken);

        var attributeDto = await mediator.Send(new GetUserAttributeQuery(attributeId), cancellationToken);
        var attribute = mapper.Map<DetailedAttributeViewModel>(attributeDto);

        var model = new EditAttributeViewModel()
        {
            Roles = roles.OrderBy(x=>x.Name),
            Attribute = attribute,
        };
        return View(model);
    }

    /// <summary>
    /// POST update user attribute method.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Redirect.</returns>
    [HttpPost("[action]/{Attribute.Id}")]
    public async Task<IActionResult> Edit(EditAttributeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Edit");
        }

        var dto = mapper.Map<EditAttributeDto>(model);

        await mediator.Send(new UpdateUserAttributeCommand(dto), cancellationToken);
        return RedirectToAction("Attributes", "Administration");
    }

    /// <summary>
    /// GET New attribute view.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> New(CancellationToken cancellationToken)
    {
        var roles = await mediator.Send(new GetRolesAndClaimsQuery(), cancellationToken);
        var model = new NewAttributeViewModel() { Roles = roles.OrderBy(role=>role.Name) };
        return View(model);
    }

    /// <summary>
    /// POST new attirubte.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Redirect.</returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> New(NewAttributeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("New");
        }

        var dto = mapper.Map<NewAttributeDto>(model);
        await mediator.Send(new CreateUserAttributeCommand(dto), cancellationToken);
        return RedirectToAction("Attributes", "Administration");
    }

    /// <summary>
    /// POST delete attribute.
    /// </summary>
    /// <param name="attributeId">Attribute ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Redirect.</returns>
    [HttpPost("[action]/{attributeId}")]
    public async Task<IActionResult> Delete(int attributeId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteAttributeCommand(attributeId), cancellationToken);
        return RedirectToAction("Attributes", "Administration");
    }
}
