using Hangfire;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using People.Infrastructure.DataAccess;
using People.Web.Infrastructure.Middlewares;
using People.Web.Infrastructure.Startup;
using People.Domain;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using People.Infrastructure.Abstractions.Interfaces;
using People.Infrastructure.Common.Settings;
using People.Infrastructure.Saml;
using People.UseCases.Common.Identity;

namespace People.Web;

/// <summary>
/// Entry point for ASP.NET Core app.
/// </summary>
public class Startup
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Entry point for web application.
    /// </summary>
    /// <param name="configuration">Global configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// Configure application services on startup.
    /// </summary>
    /// <param name="services">Services to configure.</param>
    /// <param name="environment">Application environment.</param>
    public void ConfigureServices(IServiceCollection services, IWebHostEnvironment environment)
    {
        // Swagger.
        services.AddSwaggerGen(new SwaggerGenOptionsSetup().Setup);

        // CORS.
        string[] frontendOrigin = null;
        services.AddCors(new CorsOptionsSetup(
            environment.IsDevelopment(),
            frontendOrigin
            ).Setup);

        // Health check.
        services.AddHealthChecks()
            .AddNpgSql(configuration["ConnectionStrings:AppDatabase"]);

        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        // MVC.
        services
            .AddControllersWithViews(options =>
            {
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddJsonOptions(new JsonOptionsSetup().Setup);
        services.Configure<ApiBehaviorOptions>(new ApiBehaviorOptionsSetup().Setup);

        // Identity.
        services.AddIdentity<Domain.Users.Entities.User, Domain.Users.Entities.AppIdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(new IdentityOptionsSetup().Setup);

        // Cookie.
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "People.AuthCookie";
            });

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = $"/auth/login";
            options.LogoutPath = $"/auth/logout";
        });

        // Add claims policy
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Permissions.Management,
                policy => policy.RequireClaim(CustomClaimTypes.Permission, Permissions.Management));
            options.AddPolicy(Permissions.AddUser,
                policy => policy.RequireClaim(CustomClaimTypes.Permission, Permissions.AddUser));
            options.AddPolicy(Permissions.GenerateReports,
                policy => policy.RequireClaim(CustomClaimTypes.Permission, Permissions.GenerateReports));
        });

        // Database.
        services.AddDbContext<AppDbContext>(
            new DbContextOptionsSetup(configuration.GetConnectionString("AppDatabase")).Setup);
        services.AddAsyncInitializer<DatabaseInitializer>();

        // Initializing default jobs.
        services.AddAsyncInitializer<HangfireJobnitializer>();

        // Logging.
        services.AddLogging(new LoggingOptionsSetup(configuration, environment).Setup);

        // HTTP client.
        services.AddHttpClient();

        // Hangfire.
        services.AddHangfire((config) =>
        {
            config
              .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
              .UseSimpleAssemblyNameTypeSerializer()
              .UseRecommendedSerializerSettings()
              .UsePostgreSqlStorage(configuration.GetConnectionString("AppDatabase"));
        });

        services.AddHangfireServer();

        // SAML settings.
        services.AddScoped<SamlSettings>(option => GetSamlSettings());
        services.AddTransient<ISamlService, SamlService>();

        // Local authorization settings.
        services.Configure<LocalAuthorizationSettings>(configuration.GetSection("LocalAuthorization"));

        // Other dependencies.
        Infrastructure.DependencyInjection.AutoMapperModule.Register(services);
        Infrastructure.DependencyInjection.ApplicationModule.Register(services, configuration);
        Infrastructure.DependencyInjection.MediatRModule.Register(services);
        Infrastructure.DependencyInjection.SystemModule.Register(services);
    }

    /// <summary>
    /// Configure web application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="environment">Application environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();

        // Swagger
        if (!environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(new SwaggerUIOptionsSetup().Setup);
        }
        app.UseStaticFiles();

        // Custom middlewares.
        app.UseMiddleware<ApiExceptionMiddleware>();

        // MVC.
        app.UseRouting();

        // CORS.
        app.UseCors(CorsOptionsSetup.CorsPolicyName);
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health-check",
                new HealthCheckOptionsSetup().Setup(
                    new HealthCheckOptions())
            );
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapHangfireDashboard();
            endpoints.MapControllers();
        });
    }

    private SamlSettings GetSamlSettings()
    {
        var localLoginSection = configuration.GetSection("LocalAuthorization");
        var localPassword = localLoginSection?["Password"];
        var samlSection = configuration.GetSection("SamlSettings");

        var samlCertificate = string.Empty;
        if (string.IsNullOrEmpty(localPassword))
        {
            var samlFileName = samlSection?["SamlCertificateFilename"] == null
                ? string.Empty
                : samlSection?["SamlCertificateFilename"];
            string path = Path.Combine(Directory.GetCurrentDirectory(), samlFileName);
            samlCertificate = File.ReadAllText(path);
        }

        var samlSettings = new SamlSettings()
        {
            SamlCertificate = samlCertificate,
            SamlEndpoint = samlSection?["SamlEndpoint"]!,
            SamlIdpApp = samlSection?["SamlIdpApp"]!,
            SiteDomain = samlSection?["SiteDomain"]!
        };

        return samlSettings;
    }
}
