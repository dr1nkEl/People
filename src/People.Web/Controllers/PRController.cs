using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.UseCases.Common.Dtos.PR;
using People.UseCases.Positions.Queries.GetPositions;
using People.UseCases.PR.CreateTemplate;
using People.UseCases.PR.CreateType;
using People.UseCases.PR.DeleteTemplate;
using People.UseCases.PR.GetTemplate;
using People.UseCases.PR.GetTemplates;
using People.UseCases.PR.GetTypes;
using People.Web.ViewModels.Position;
using People.Web.ViewModels.PR;

namespace People.Web.Controllers;

/// <summary>
/// Perfomance review controller.
/// </summary>
[Route("admin/[controller]")]
public class PRController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="mapper">Mapper.</param>
    public PRController(IMediator mediator, IMapper mapper)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    /// <summary>
    /// GET create new type view.
    /// </summary>
    /// <returns>View.</returns>
    [HttpGet("[action]")]
    public IActionResult Type()
    {
        return View();
    }

    /// <summary>
    /// POST create new type.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Redirect.</returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> Type(NewReviewTypeViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var dto = mapper.Map<NewReviewTypeDto>(model);
        await mediator.Send(new CreateReviewTypeCommand(dto), cancellationToken);
        return RedirectToAction(nameof(AdministrationController.PRTemplates), nameof(AdministrationController).Replace("Controller", null));
    }

    /// <summary>
    /// GET create new template view.
    /// </summary>
    /// <returns>View.</returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> New(CancellationToken cancellationToken)
    {
        var model = await GetTemplateOptionsVMAsync(cancellationToken);
        return View(model);
    }

    /// <summary>
    /// GET Edit template view.
    /// </summary>
    /// <param name="templateId">Template ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet("[action]/{templateId}")]
    public async Task<IActionResult> Edit(int templateId, CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new GetPRTemplateQuery(templateId), cancellationToken);
        var item = mapper.Map<TemplateExtendedViewModel>(dto);
        var optionsModel = await GetTemplateOptionsVMAsync(cancellationToken);
        var model = new EditTemplateViewModel
        {
            Template = item,
            Options = optionsModel,
        };
        return View(model);
    }

    /// <summary>
    /// POST delete template.
    /// </summary>
    /// <param name="templateId">Template ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Redirect.</returns>
    [HttpPost("[action]/{templateId}")]
    public async Task<IActionResult> Delete(int templateId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteTemplateCommand(templateId), cancellationToken);
        return RedirectToAction(nameof(AdministrationController.PRTemplates), nameof(AdministrationController).Replace("Controller", null));
    }

    private async Task<TemplateOptionsViewModel> GetTemplateOptionsVMAsync(CancellationToken cancellationToken)
    {
        var positionsDto = await mediator.Send(new GetPositionsQuery(), cancellationToken);
        var positions = mapper.Map<IEnumerable<PositionViewModel>>(positionsDto);

        var typesDto = await mediator.Send(new GetReviewTypesQuery(), cancellationToken);
        var types = mapper.Map<IEnumerable<ReviewTypeViewModel>>(typesDto);

        var templatesDto = await mediator.Send(new GetPRTemplatesQuery(), cancellationToken);
        var templates = mapper.Map<IEnumerable<TemplateViewModel>>(templatesDto);

        var model = new TemplateOptionsViewModel
        {
            Positions = positions.OrderBy(pos=>pos.Name),
            ReviewTypes = types.OrderBy(type=>type.Name),
            Templates = templates.OrderBy(template=>template.Name),
        };
        return model;
    }
}
