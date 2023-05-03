using System.Threading;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Domain.Reviews.Entities;
using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.PR;
using People.UseCases.Positions.Queries.GetPositions;
using People.UseCases.PR.CreateTemplate;
using People.UseCases.PR.CreateType;
using People.UseCases.PR.DeleteTemplate;
using People.UseCases.PR.GetFinishedReviews;
using People.UseCases.PR.GetReply;
using People.UseCases.PR.GetSetReviews;
using People.UseCases.PR.GetTemplate;
using People.UseCases.PR.GetTemplates;
using People.UseCases.PR.GetTypes;
using People.UseCases.PR.SetReview;
using People.UseCases.Reviews.GetReview;
using People.UseCases.Users;
using People.UseCases.Users.GetUserById;
using People.Web.ViewModels;
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

    /// <summary>
    /// POST set review.
    /// </summary>
    /// <param name="model"><see cref="SetViewModel"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> SetReview([FromForm]SetViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await mediator.Send(new SetReviewCommand(model.ReviewedUserId.Value, model.TemplateId.Value, model.Deadline, model.ReviewedByUsersIds), cancellationToken);

        return RedirectToAction(nameof(AdministrationController.PRTemplates), nameof(AdministrationController).Replace("Controller", null));
    }

    /// <summary>
    /// GET set review page.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> SetReview(CancellationToken cancellationToken)
    {
        var usersDto = await mediator.Send(new GetAllUsersDetailedQuery(), cancellationToken);
        var users = mapper.Map<IEnumerable<UserDetailedViewModel>>(usersDto);

        var templatesDto = await mediator.Send(new GetPRTemplatesQuery(), cancellationToken);
        var templates = mapper.Map<IEnumerable<TemplateViewModel>>(templatesDto);

        return View(new SetViewModel() { ReviewTemplates = templates, Users = users });
    }

    /// <summary>
    /// GET set reviews.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("[action]")]
    public async Task<IActionResult> Reviews(CancellationToken cancellationToken)
    {
        var reviews = await mediator.Send(new GetSetReviewsQuery(), cancellationToken);
        return View(reviews);
    }

    /// <summary>
    /// GET page with reponse of given user.
    /// </summary>
    /// <param name="prId">ID of PR.</param>
    /// <param name="userId">ID of user.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    [HttpGet("[action]/{prId}/{userId}")]
    public async Task<IActionResult> Feedback([FromRoute]int prId, [FromRoute]int userId, CancellationToken cancellationToken)
    {
        var review = await mediator.Send(new GetReviewQuery(prId), cancellationToken);

        var userFeedback = await mediator.Send(new GetUserByIdQuery() { UserId = userId }, cancellationToken);
        var feedBackToUser = await mediator.Send(new GetUserByIdQuery() { UserId = review.ReviewedUserId }, cancellationToken);
        var reply = await mediator.Send(new GetReplyQuery(prId, userId));

        return View(new FeedbackViewModel() { FeedbackUser = mapper.Map<User>(userFeedback), Reply = reply, ReviewedUser = mapper.Map<User>(feedBackToUser)});
    }

    /// <summary>
    /// GET finished reviews page.
    /// </summary>
    [HttpGet("[action]")]
    public async Task<IActionResult> Finished(CancellationToken cancellationToken)
    {
        return View(await mediator.Send(new GetFinishedReviewsQuery(), cancellationToken));
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
