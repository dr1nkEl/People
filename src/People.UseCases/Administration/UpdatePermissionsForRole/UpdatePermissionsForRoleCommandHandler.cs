using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using People.Domain.Users.Entities;
using People.UseCases.Common.Identity;
using Saritasa.Tools.Domain.Exceptions;

namespace People.UseCases.Administration.UpdatePermissionsForRole;

/// <summary>
/// Update claim for role handler.
/// </summary>
public class UpdatePermissionsForRoleCommandHandler : AsyncRequestHandler<UpdatePermissionsForRoleCommand>
{
    private RoleManager<AppIdentityRole> roleManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdatePermissionsForRoleCommandHandler(RoleManager<AppIdentityRole> roleManager)
    {
        this.roleManager = roleManager;
    }

    /// <inheritdoc />
    protected override async Task Handle(UpdatePermissionsForRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
        var allPermissions = Permissions.GetAllPermissions();

        if (role == null || !allPermissions.Contains(request.ClaimValue))
        {
            throw new DomainException("Some parameters aren't correct");
        }
        if (request.IsEnabled)
        {
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, request.ClaimValue));
        }
        else
        {
            await roleManager.RemoveClaimAsync(role, new Claim(CustomClaimTypes.Permission, request.ClaimValue));
        }
    }
}
