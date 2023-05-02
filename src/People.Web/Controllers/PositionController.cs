using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.UseCases.Positions.Commands;
using People.UseCases.Positions.Queries.GetPositionsWithInfo;
using People.UseCases.Positions.Queries.GetPositions;
using Saritasa.Tools.Domain.Exceptions;
using AutoMapper;
using People.UseCases.Common.Dtos.Position;
using People.Web.ViewModels.Position;

namespace People.Web.Controllers;

/// <summary>
/// Position controller class.
/// </summary>
public class PositionController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="mapper">Mapper.</param>
    public PositionController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    /// <summary>
    /// Main page.
    /// </summary>
    /// <returns>View.</returns>
    [HttpGet]
    [Route("/admin/positions")]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Add position.
    /// </summary>
    /// <param name="position">Position to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>HTTP Status code.</returns>
    [HttpPost]
    public async Task<IActionResult> Add([FromForm]CreateOrEditPositionDto position, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(new AddOrEditPositionCommand(position), cancellationToken);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get Add modal view.
    /// </summary>
    /// <param name="viewModel">View model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet]
    public async Task<IActionResult> Add(CreatePositionViewModel viewModel, CancellationToken cancellationToken)
    {
        var positions = await mediator.Send(new GetPositionsQuery(), cancellationToken);
        viewModel = new CreatePositionViewModel() { Positions = positions };
        return View(viewModel);
    }

    /// <summary>
    /// Edit a positon.
    /// </summary>
    /// <param name="position">Edited position.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>HTTP Status code.</returns>
    [HttpPut]
    public async Task<IActionResult> Edit([FromForm]CreateOrEditPositionDto position, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(new AddOrEditPositionCommand(position), cancellationToken);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get Edit modal view.
    /// </summary>
    /// <param name="positionId">Position id.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet]
    public async Task<IActionResult> Edit([FromQuery]int positionId, EditPositionViewModel viewModel, CancellationToken cancellationToken)
    {
        var positions = await mediator.Send(new GetPositionsWithInfoQuery(), cancellationToken);
        var editedPosition = positions.FirstOrDefault(position => position.Id == positionId);
        var positionDtos = mapper.Map<IEnumerable<PositionDto>>(positions.Where(position => position.Id != positionId));
        viewModel.EditedPosition = editedPosition;
        viewModel.Positions = positionDtos;
        return View(viewModel);
    }

    /// <summary>
    /// Get Delete modal view.
    /// </summary>
    /// <param name="positionId">Position id.</param>
    /// <param name="viewModel">View model.</param>
    /// <returns>View.</returns>
    [HttpGet]
    public IActionResult Delete([FromQuery]int positionId, DeletePositionViewModel viewModel)
    {
        viewModel.Id = positionId;
        return View(viewModel);
    }

    /// <summary>
    /// Delete position by name.
    /// </summary>
    /// <param name="positionId">Position id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>HTTP Status code.</returns>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery]int positionId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeletePositionCommand(positionId), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Get positions table view.
    /// </summary>
    /// <param name="viewModel">View model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet]
    public async Task<IActionResult> List(ListPositionViewModel viewModel, CancellationToken cancellationToken)
    {
        var positions = await mediator.Send(new GetPositionsWithInfoQuery(), cancellationToken);
        viewModel.Positions = positions;
        return View(viewModel);
    }
}
