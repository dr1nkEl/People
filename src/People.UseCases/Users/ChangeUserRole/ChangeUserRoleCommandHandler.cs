using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Users.ChangeUserRole;

/// <inheritdoc cref="ChangeUserRoleCommand"/>
public class ChangeUserRoleCommandHandler : AsyncRequestHandler<ChangeUserRoleCommand>
{
    private RoleManager<AppIdentityRole> roleManager;
    private UserManager<User> userManager;
    private ILogger<ChangeUserRoleCommandHandler> logger;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ChangeUserRoleCommandHandler(RoleManager<AppIdentityRole> roleManager, 
        UserManager<User> userManager, 
        ILogger<ChangeUserRoleCommandHandler> logger,
        ILoggedUserAccessor loggedUserAccessor)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
        this.logger = logger;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc />
    protected override async Task Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        if (!loggedUserAccessor.HasClaim(CustomClaimTypes.Permission, Permissions.Management))
        {
            throw new ForbiddenException("Role for specific user can edit only user with management permission.");
        }

        var user = await userManager.Users
            .GetAsync(user => user.Id == request.UserId, cancellationToken);

        var newRole = await roleManager.Roles
            .GetAsync(role => role.Id == request.RoleIdToSet, cancellationToken);

        // Remove old role/roles.
        var currentUserRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentUserRoles);

        // Set new role.
        await userManager.AddToRoleAsync(user, newRole.Name);
        logger.LogInformation("Role = {Role} set to user = {user}", newRole, user);
    }
}
