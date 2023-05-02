using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Domain.Reviews.Entities;
using People.UseCases.Common.Dtos.PR;
using People.UseCases.PR.CreateTemplate;
using People.UseCases.PR.GetTemplate;
using People.UseCases.PR.GetTemplatesForUser;
using People.UseCases.PR.PatchTemplate;
using People.Web.Services;
using People.Web.ViewModels;
using People.Web.ViewModels.PR;

namespace People.Web.Controllers.Api;

/// <summary>
/// Perfomance review API controller.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PerfomanceReview")]
public class PRController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mapper">Mapper.</param>
    /// <param name="mediator">Mediator.</param>
    public PRController(IMapper mapper, IMediator mediator)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    /// <summary>
    /// GET question types action.
    /// </summary>
    /// <returns>Question types.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public IEnumerable<OptionViewModel> GetQuestionAnswerTypes()
    {
        return EnumService.ParseEnum<AnswerType>().OrderBy(type=>type.Text);
    }

    /// <summary>
    /// GET interval types action.
    /// </summary>
    /// <returns>Interval types.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public IEnumerable<OptionViewModel> GetIntervalTypes()
    {
        return EnumService.ParseEnum<Interval>().OrderBy(interval=>interval.Text);
    }

    /// <summary>
    /// POST create new template.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>HTTP response code.</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> NewTemplate([FromForm]TemplateExtendedViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var item = mapper.Map<NewTemplateDto>(model);

        await mediator.Send(new CreateTemplateCommand(item), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// GET template action.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Template.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<ActionResult<TemplateExtendedViewModel>> GetTemplate([FromQuery]int? id, CancellationToken cancellationToken)
    {
        if (id == null)
        {
            return Ok(new TemplateExtendedViewModel());
        }

        var dto = await mediator.Send(new GetPRTemplateQuery(id.Value), cancellationToken);
        var item = mapper.Map<TemplateExtendedViewModel>(dto);
        return Ok(item);
    }

    /// <summary>
    /// GET templates of user.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<TemplateViewModel>>> GetTemplatesForUser([FromQuery]int userId, CancellationToken cancellationToken)
    {
        var templatesDto = await mediator.Send(new GetPrTemplatesForUserQuery(userId), cancellationToken);
        return Ok(mapper.Map<IEnumerable<TemplateViewModel>>(templatesDto));
    }

    /// <summary>
    /// PATCH template action.
    /// </summary>
    /// <param name="model">Model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>HTTP response code.</returns>
    [HttpPatch]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> PatchReviewTemplate([FromForm] TemplateExtendedViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var mapped = mapper.Map<PRTemplateDetailedDto>(model);
        await mediator.Send(new PatchTemplateCommand(mapped), cancellationToken);
        return Ok();
    }
}
