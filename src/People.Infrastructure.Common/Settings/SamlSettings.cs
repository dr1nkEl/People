namespace People.Infrastructure.Common.Settings;

/// <summary>
/// SAML settings.
/// </summary>
public record SamlSettings
{
    /// <summary>
    /// SAML certificate.
    /// </summary>
    public string SamlCertificate { get; init; }

    /// <summary>
    /// SAML endpoint to redirect.
    /// </summary>
    public string SamlEndpoint { get; init; }

    /// <summary>
    /// SAML Identity provider endpoint.
    /// </summary>
    public string SamlIdpApp { get; init; }

    /// <summary>
    /// Site domain.
    /// </summary>
    public string SiteDomain { get; init; }
}
