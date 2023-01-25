using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.UseCases.Administration.Attributes.GetAttributeOptions;
using People.Web.ViewModels.Attribute;

namespace People.Web.Controllers.Api;

/// <summary>
/// Attribute API controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Attribute")]
public class AttributeController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="mapper">Mapper.</param>
    public AttributeController(IMediator mediator, IMapper mapper)
    {
        this.mapper = mapper;
        this.mediator = mediator;
    }

    /// <summary>
    /// GET attribute options.
    /// </summary>
    /// <param name="attributeId">Attribute ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Attribute options.</returns>
    [HttpGet("[action]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<AttributeOptionViewModel>>> GetAttributeOptions([FromQuery]int attributeId, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var items = await mediator.Send(new GetAttributeOptionsQuery(attributeId), cancellationToken);
        return Ok(mapper.Map<IEnumerable<AttributeOptionViewModel>>(items));
    }
}
