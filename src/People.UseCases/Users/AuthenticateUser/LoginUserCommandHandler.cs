using System.Data;
using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using People.Domain.Users.Entities;
using People.Infrastructure.Common.Settings;
using Saritasa.Tools.Domain.Exceptions;

namespace People.UseCases.Users.AuthenticateUser;

/// <summary>
/// Handler for <see cref="LoginUserCommand" />.
/// </summary>
internal class LoginUserCommandHandler : AsyncRequestHandler<LoginUserCommand>
{
    private readonly SignInManager<User> signInManager;
    private readonly ILogger<LoginUserCommandHandler> logger;
    private readonly IMapper mapper;
    private readonly LocalAuthorizationSettings localAuthorizationSettings;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="signInManager">Sign in manager.</param>
    /// <param name="logger">Logger.</param>
    /// <param name="mapper">Mapper.</param>
    /// <param name="localAuthorizationSettings">Authorization settings.</param>
    public LoginUserCommandHandler(
        SignInManager<User> signInManager,
        ILogger<LoginUserCommandHandler> logger,
        IMapper mapper,
        IOptions<LocalAuthorizationSettings> localAuthorizationSettings)
    {
        this.signInManager = signInManager;
        this.logger = logger;
        this.mapper = mapper;
        this.localAuthorizationSettings = localAuthorizationSettings.Value;
    }

    /// <inheritdoc />
    protected override async Task Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Get user and log.
        var user = await signInManager.UserManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new NotFoundException("User with the entered data was not found.");
        }

        if (!string.IsNullOrEmpty(localAuthorizationSettings.Password) && localAuthorizationSettings.Password == request.Password)
        {
            logger.LogInformation("User with email {email} has logged in.", user.Email);
            // Set cookie.
            await signInManager.SignInAsync(user, request.RememberMe);
            return;
        }

        var signInResult = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

        if (!signInResult.Succeeded)
        {
            throw new DomainException("Check password");
        }

        // Update last login date.
        user.LastLogin = DateTime.UtcNow;
        await signInManager.UserManager.UpdateAsync(user);
    }
}
