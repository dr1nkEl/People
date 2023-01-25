namespace People.Domain;

/// <summary>
/// Application settings.
/// </summary>
public record ApplicationSettings
{
    /// <summary>
    /// CRM API endpoint.
    /// </summary>
    public string CrmApiEndpoint { get; init; }

    /// <summary>
    /// CRM authorization token.
    /// </summary>
    public string CrmApiToken { get; init; }
}
