using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using People.Infrastructure.Abstractions.Interfaces;
using People.Infrastructure.Common.Settings;
using People.Infrastructure.Saml;
using People.UseCases.Common.Dtos.User;
using People.UseCases.Users.AuthenticateUser;
using People.UseCases.Users.GetUserById;
using People.Web.Infrastructure.Web;
using People.Web.ViewModels;
using Saritasa.Tools.Domain.Exceptions;

namespace People.Web.Controllers;

/// <summary>
/// Authentication controller.
/// </summary>
[Route("auth")]
[ApiExplorerSettings(GroupName = "auth")]
[AllowAnonymous]
public class AuthController : Controller
{
    private readonly IMediator mediator;
    private readonly ISamlService samlService;
    private readonly LocalAuthorizationSettings localAuthorizationSettings;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AuthController(IMediator mediator, ISamlService samlService, IOptions<LocalAuthorizationSettings> localAuthorizationSettings, IMapper mapper)
    {
        this.mediator = mediator;
        this.samlService = samlService;
        this.localAuthorizationSettings = localAuthorizationSettings?.Value;
        this.mapper = mapper;
    }

    #region Authentication

    /// <summary>
    /// Get current logged user info.
    /// </summary>
    /// <returns>Current logged user info.</returns>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpGet]
    [Authorize]
    public async Task<UserDto> GetMe(CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery
        {
            UserId = User.GetCurrentUserId()
        };
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Redirect to SAML identity provider or show login page if website opened local.
    /// </summary>
    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> Login(CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(localAuthorizationSettings.Password))
        {
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        var redirectUrl = await samlService.GetRedirectLoginUrlAsync(cancellationToken);
        return Redirect(redirectUrl);
    }

    /// <summary>
    /// Local login in application.
    /// </summary>
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([Required] LoginViewModel model, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(localAuthorizationSettings.Password))
        {
            throw new ForbiddenException("Сan't login to a non-local site.");
        }
        bool loginSucceed = true;
        try
        {
            var command = mapper.Map<LoginUserCommand>(model);
            await mediator.Send(command, cancellationToken);
        }
        catch (DomainException)
        {
            loginSucceed = false;
        }

        if (!loginSucceed)
        {
            ModelState.AddModelError("", "Email or password is incorrect");
            return View();
        }

        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Endpoint for handle SAML response from Idp.
    /// </summary>
    [HttpPost]
    [Route("saml")]
    [AllowAnonymous]
    public async Task<IActionResult> SamlResponseConsumer(CancellationToken cancellationToken)
    {
        var samlResponse = Request.Form["SAMLResponse"];
        var email = await samlService.GetEmailAsync(samlResponse, cancellationToken);
        await mediator.Send(new LoginUserCommand()
        {
            Email = email
        }, cancellationToken);

        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Endpoint for handle SAML response from Idp.
    /// </summary>
    [HttpPost]
    [Route("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await mediator.Send(new LogoutUserCommand(), cancellationToken);

        if (string.IsNullOrEmpty(localAuthorizationSettings.Password))
        {
            var url = await samlService.GetRedirectLogoutUrlAsync(cancellationToken);
            return Redirect(url);
        }

        return RedirectToAction("Index", "Home");
    }

    #endregion
}
