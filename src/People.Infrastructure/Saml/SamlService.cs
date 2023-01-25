using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using People.Infrastructure.Abstractions.Interfaces;
using People.Infrastructure.Common.Settings;
using Saritasa.Tools.Domain.Exceptions;
using ValidationException = Saritasa.Tools.Domain.Exceptions.ValidationException;

namespace People.Infrastructure.Saml;

/// <summary>
/// Saml service.
/// </summary>
public class SamlService : ISamlService
{
    private SamlSettings settings;
    private ILogger<SamlService> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="settings">Settings.</param>
    public SamlService(SamlSettings settings, ILogger<SamlService> logger)
    {
        this.settings = settings;
        this.logger = logger;
    }


    /// <inheritdoc />
    public async Task<string> GetRedirectLoginUrlAsync(CancellationToken cancellationToken)
    {
        var samlEndpoint = settings.SamlEndpoint;
        var consumerUrl = "";
        if (settings.SiteDomain.EndsWith("/"))
        {
            consumerUrl = settings.SiteDomain + "auth/saml";
        }
        else
        {
            consumerUrl = settings.SiteDomain + "/auth/saml";
        }

        var request = new SamlRequest(settings.SiteDomain, consumerUrl);

        return request.GetRedirectUrl(samlEndpoint);
    }

    /// <inheritdoc />
    public async Task<string> GetRedirectLogoutUrlAsync(CancellationToken cancellationToken)
    {
        return settings.SamlIdpApp;
    }

    /// <inheritdoc />
    public async Task<string> GetEmailAsync(string samlResponse, CancellationToken cancellationToken)
    {
        logger.LogInformation("Got response from saml identity provider.");
        var response = new SamlResponse(settings.SamlCertificate, samlResponse);
        if (!response.IsValid())
        {
            logger.LogError("Saml response isn't valid.");
            throw new ValidationException("Saml response isn't valid.");
        }
        var email = response.GetEmail();
        logger.LogInformation("Identity provider return user email: {email}", email);
        return email;
    }
}
