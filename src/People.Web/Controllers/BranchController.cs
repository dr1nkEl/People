using Microsoft.AspNetCore.Mvc;
using People.Web.ViewModels;
using MediatR;
using People.UseCases.Branches;
using People.UseCases.Users;
using AutoMapper;
using People.Web.ViewModels.User;

namespace People.Web.Controllers;

/// <summary>
/// Branch controller.
/// </summary>
[Route("[controller]")]
public class BranchController : Controller
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public BranchController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    /// <summary>
    /// GET Action method to show index view.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var branchesdtos = await mediator.Send(new GetAllBranchesQuery(), cancellationToken);
        var branches = mapper.Map<IEnumerable<BranchViewModel>>(branchesdtos);
        var model = new BranchesViewModel() { Branches = branches.OrderBy(x=>x.Name) };
        return View(model);
    }

    /// <summary>
    /// GET Action method to show edit view.
    /// </summary>
    /// <param name="branchId">Branch Id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpGet("[action]/{branchId}")]
    public async Task<IActionResult> Edit(int branchId, CancellationToken cancellationToken)
    {
        var users = await mediator.Send(new GetAllUsersQuery(), cancellationToken);
        var mappedUsers = users.Select(x => mapper.Map<UserViewModel>(x));

        var branchDto = await mediator.Send(new GetBranchByIdQuery(branchId), cancellationToken);

        var branch = mapper.Map<BranchViewModel>(branchDto);

        var model = new BranchEditViewModel()
        {
            Branch = branch,
            Users = mappedUsers.OrderBy(x=>x.FullName)
        };

        return View(model);
    }

    /// <summary>
    /// POST Action method to proceed edit.
    /// </summary>
    /// <param name="model">Branch edit view model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>View.</returns>
    [HttpPost("[action]/{Branch.Id}")]
    public async Task<IActionResult> Edit(BranchEditViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await mediator.Send(new UpdateBranchDirectorCommand(model.Branch.Id, model.Branch.DirectorId), cancellationToken);

        return RedirectToAction(nameof(Index));
    }
}
