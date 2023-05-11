using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;

namespace People.UseCases.Administration.GetRolesOfUser;

/// <summary>
/// Handler for <see cref="GetRolesOfUserQuery"/>.
/// </summary>
internal class GetRolesOfUserQueryHandler : IRequestHandler<GetRolesOfUserQuery, IEnumerable<AppIdentityRole>>
{
    private readonly RoleManager<AppIdentityRole> roleManager;
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="roleManager"><see cref="RoleManager{TRole}"/>.</param>
    /// <param name="userManager"></param>
    public GetRolesOfUserQueryHandler(RoleManager<AppIdentityRole> roleManager, UserManager<User> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<AppIdentityRole>> Handle(GetRolesOfUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        var rolesString = await userManager.GetRolesAsync(user);
        var roles = await roleManager.Roles.Where(x => rolesString.Contains(x.Name)).ToListAsync(cancellationToken);
        return roles;
    }
}
