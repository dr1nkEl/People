using System.Security.Claims;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.DataAccess;
using People.UseCases.Common.Identity;

namespace People.Web.Infrastructure.Startup;

/// <summary>
/// Contains database migration helper methods.
/// </summary>
internal sealed class DatabaseInitializer : IAsyncInitializer
{
    private const string AdminRole = "ALL";
    private readonly IReadOnlyCollection<Branch> branches = new List<Branch>()
    {
        new() { Name = "Ekaterinburg" },
        new() { Name = "Chelyabinsk" },
        new() { Name = "Moscow" },
        new() { Name = "Paris" },
        new() { Name = "Brazilia" },
    };

    private readonly AppDbContext appDbContext;
    private readonly RoleManager<AppIdentityRole> roleManager;

    /// <summary>
    /// Database initializer. Performs migration and data seed.
    /// </summary>
    /// <param name="appDbContext">Data context.</param>
    /// <param name="roleManager">Role manager.</param>
    public DatabaseInitializer(AppDbContext appDbContext,
        RoleManager<AppIdentityRole> roleManager)
    {
        this.appDbContext = appDbContext;
        this.roleManager = roleManager;
    }

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        await appDbContext.Database.MigrateAsync();
        await AddDefaultRolesAsync();
        await AddDefaultBranchesAsync();
    }

    private async Task AddDefaultBranchesAsync()
    {
        var count = await appDbContext.Branches.CountAsync();

        if (count == 0)
        {
            await appDbContext.Branches.AddRangeAsync(branches);
            await appDbContext.SaveChangesAsync();
        }
    }

    private async Task AddDefaultRolesAsync()
    {
        var userRoleName = "User";
        if (await roleManager.FindByNameAsync(userRoleName) == null)
        {
            var userRole = new AppIdentityRole();
            await roleManager.SetRoleNameAsync(userRole, userRoleName);
            await roleManager.CreateAsync(userRole);
        }

        var adminRoleName = "Admin";
        if (await roleManager.FindByNameAsync(adminRoleName) == null)
        {
            var adminRole = new AppIdentityRole();
            adminRole.InheritedRole = await roleManager.Roles.Where(role => role.Name == userRoleName).FirstOrDefaultAsync();
            await roleManager.SetRoleNameAsync(adminRole, adminRoleName);
            await roleManager.UpdateNormalizedRoleNameAsync(adminRole);
            await roleManager.CreateAsync(adminRole);
            await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Permissions.AddUser));
            await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Permissions.GenerateReports));
            await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Permissions.Management));
        }
    }
}
