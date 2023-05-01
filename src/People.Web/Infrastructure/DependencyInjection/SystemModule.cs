using Microsoft.AspNetCore.Mvc.Rendering;
using People.Infrastructure;
using People.Infrastructure.Abstractions.Interfaces;
using People.Infrastructure.DataAccess;
using People.UseCases.Users.AuthenticateUser;
using People.Web.Infrastructure.Jwt;
using People.Web.Infrastructure.Web;

namespace People.Web.Infrastructure.DependencyInjection;

/// <summary>
/// System specific dependencies.
/// </summary>
internal static class SystemModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IJsonHelper, SystemTextJsonHelper>();
        services.AddScoped<IAuthenticationTokenService, SystemJwtTokenService>();
        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<ILoggedUserAccessor, LoggedUserAccessor>();
    }
}
