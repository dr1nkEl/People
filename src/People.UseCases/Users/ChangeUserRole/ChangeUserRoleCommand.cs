using System.ComponentModel.DataAnnotations;
using MediatR;

namespace People.UseCases.Users.ChangeUserRole;

/// <summary>
/// Command for change user role.
/// </summary>
public record ChangeUserRoleCommand : IRequest
{
    /// <summary>
    /// User id.
    /// </summary>
    [Required]
    public int UserId { get; init; }

    /// <summary>
    /// New role id for user.
    /// </summary>
    [Required]
    public int RoleIdToSet { get; init; }
}
