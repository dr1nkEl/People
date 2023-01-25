namespace People.Infrastructure.Common.Settings;

/// <summary>
/// Authorization on local website.
/// </summary>
public record LocalAuthorizationSettings
{
    /// <summary>
    /// Password for authorize on local website.
    /// </summary>
    public string Password { get; init; }
}
