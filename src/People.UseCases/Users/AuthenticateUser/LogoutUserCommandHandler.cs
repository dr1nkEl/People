using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using People.Domain.Users.Entities;

namespace People.UseCases.Users.AuthenticateUser;

/// <summary>
/// Logout user.
/// </summary>
internal class LogoutUserCommandHandler : AsyncRequestHandler<LogoutUserCommand>
{
    private readonly SignInManager<User> signInManager;
    private readonly ILogger<LogoutUserCommandHandler> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public LogoutUserCommandHandler(SignInManager<User> signInManager, ILogger<LogoutUserCommandHandler> logger)
    {
        this.signInManager = signInManager;
        this.logger = logger;
    }

    /// <inheritdoc />
    protected override async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();
    }
}
