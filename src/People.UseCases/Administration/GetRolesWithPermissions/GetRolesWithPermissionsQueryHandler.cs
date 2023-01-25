using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.Administration;
using People.UseCases.Common.Identity;

namespace People.UseCases.Administration.GetRolesWithPermissions;

/// <summary>
/// Get roles and claims query handler.
/// </summary>
internal class GetRolesWithPermissionsQueryHandler : IRequestHandler<GetRolesAndClaimsQuery, IEnumerable<RoleDto>>
{
    private readonly RoleManager<AppIdentityRole> roleManager;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="roleManager">Role manager.</param>
    /// <param name="mapper">Mapper.</param>
    public GetRolesWithPermissionsQueryHandler(RoleManager<AppIdentityRole> roleManager, IMapper mapper)
    {
        this.roleManager = roleManager;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RoleDto>> Handle(GetRolesAndClaimsQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles
            .OrderBy(role => role.Id)
            .ToListAsync(cancellationToken);

        var allPermissions = Permissions.GetAllPermissions();

        var roleDtos = new List<RoleDto>();
        foreach (var role in roles)
        {
            var roleClaims = await roleManager.GetClaimsAsync(role);
            roleClaims.Where(claim => claim.Type == CustomClaimTypes.Permission);
            var permissionDtosForRole = allPermissions
                .Select(permissionDtoValue => new PermissionDto()
                {
                    Name = permissionDtoValue,
                    IsEnable = roleClaims.Any(claim => claim.Value == permissionDtoValue)
                });

            var roleDtoWithPermissions = mapper.Map<RoleDto>(role);
            roleDtoWithPermissions = roleDtoWithPermissions with
            {
                Permissions = permissionDtosForRole
            };

            roleDtos.Add(roleDtoWithPermissions);
        }

        return roleDtos;
    }
}
