using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using People.Domain.Users.Entities;
using Saritasa.Tools.Domain.Exceptions;

namespace People.UseCases.Users.CreateUser;

/// <summary>
/// Handler for <see cref="CreateUserCommand"/>.
/// </summary>
internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager"><see cref="UserManager{TUser}"/>.</param>
    public CreateUserCommandHandler(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    /// <inheritdoc/>
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
            EmailConfirmed = true,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BranchId = request.BranchId
        };
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new DomainException(JsonSerializer.Serialize(result.Errors));
        }
        return user.Id;
    }
}
