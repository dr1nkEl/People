using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.Domain.Exceptions;

namespace People.UseCases.Users.ChangePasswordCommand;

/// <summary>
/// Handler for <see cref="ChangePasswordCommand"/>.
/// </summary>
internal class ChangePasswordCommandHandler : AsyncRequestHandler<ChangePasswordCommand>
{
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager"><see cref="UserManager{TUser}"/>.</param>
    public ChangePasswordCommandHandler(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    /// <inheritdoc/>
    protected override async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        var res = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        if (!res.Succeeded)
        {
            throw new DomainException(JsonSerializer.Serialize(res.Errors));
        }
    }
}
