using MediatR;
using People.UseCases.Common.Dtos.Administration;

namespace People.UseCases.Administration.GetRolesWithPermissions;

/// <summary>
/// Get roles and claims query.
/// </summary>
public record GetRolesAndClaimsQuery : IRequest<IEnumerable<RoleDto>>;
