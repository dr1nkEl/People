using Microsoft.Extensions.Options;
using RestSharp;
using People.Domain;
using People.Infrastructure.Abstractions.Interfaces;
using People.Infrastructure.Common.Crm.Dto;
using Saritasa.Tools.Domain.Exceptions;

namespace People.Infrastructure;

/// <inheritdoc/>.
public class CrmAccessor : ICrmAccessor
{
    private const string GetBranchesUrl = "api/misc/branches";
    private const string GetActiveUsersUrl = "api/users/employeeslist";
    private readonly ApplicationSettings appSettings;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="settings">Settings.</param>
    public CrmAccessor(IOptions<ApplicationSettings> settings)
    {
        this.appSettings = settings.Value;
    }

    /// <inheritdoc/>.
    public async Task<BranchesDto> GetBranchesAsync(CancellationToken cancellationToken)
    {
        var request = new RestRequest(GetBranchesUrl);
        return await ExecuteGetRequest<BranchesDto>(request, cancellationToken);
    }

    /// <inheritdoc/>.
    public async Task<UsersDto> GetUsersAsync(CancellationToken cancellationToken)
    {
        var request = new RestRequest(GetActiveUsersUrl);
        request.AddParameter("date", DateTime.UtcNow.ToString("yyyy.MM.dd"));
        request.AddParameter("statuses", "Active");
        return await ExecuteGetRequest<UsersDto>(request, cancellationToken);
    }

    private async Task<T> ExecuteGetRequest<T>(RestRequest request, CancellationToken cancellationToken)
    {
        using var restClient = GetConfiguredClient();

        var response = await restClient.ExecuteGetAsync<T>(request, cancellationToken);
        if (!response.IsSuccessful)
        {
            throw new InfrastructureException(new HttpRequestException(response.StatusDescription));
        }
        return response.Data;
    }

    private RestClient GetConfiguredClient()
    {
        var client = new RestClient(appSettings.CrmApiEndpoint);
        client.AddDefaultHeader("Authorization", $"Bearer {appSettings.CrmApiToken}");
        return client;
    }
}
