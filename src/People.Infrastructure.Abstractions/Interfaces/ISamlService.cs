namespace People.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Saml service.
/// </summary>
public interface ISamlService
{
    /// <summary>
    /// Get URL for redirect to sso for login.
    /// </summary>
    /// <returns>URL for redirect.</returns>
    Task<string> GetRedirectLoginUrlAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get URL for redirect to sso for logout.
    /// </summary>
    /// <returns>Url for redirect.</returns>
    Task<string> GetRedirectLogoutUrlAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get email from saml response.
    /// </summary>
    Task<string> GetEmailAsync(string samlResponse, CancellationToken cancellationToken = default);
}
