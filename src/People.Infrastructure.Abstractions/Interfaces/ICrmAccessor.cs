using People.Infrastructure.Common.Crm.Dto;

namespace People.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// CRM accessor.
/// </summary>
public interface ICrmAccessor
{
    /// <summary>
    /// Get branches.
    /// </summary>
    /// <returns>Branches DTO.</returns>
    Task<BranchesDto> GetBranchesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Get users.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Users DTO.</returns>
    Task<UsersDto> GetUsersAsync(CancellationToken cancellationToken);
}
