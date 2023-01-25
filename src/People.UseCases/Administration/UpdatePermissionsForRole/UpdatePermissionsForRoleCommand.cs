using System.ComponentModel.DataAnnotations;
using MediatR;

namespace People.UseCases.Administration.UpdatePermissionsForRole;

/// <summary>
/// Update claim for role.
/// </summary>
public record UpdatePermissionsForRoleCommand : IRequest
{
    /// <summary>
    /// Role name.
    /// </summary>
    [Required]
    public int RoleId { get; init; }

    /// <summary>
    /// Claim value.
    /// </summary>
    [Required]
    public string ClaimValue { get; init; }

    /// <summary>
    /// Claim status for role.
    /// </summary>
    public bool IsEnabled { get; init; }
}
