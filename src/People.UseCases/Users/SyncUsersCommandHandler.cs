using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.Infrastructure.Common.Crm.Dto;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Users;

/// <inheritdoc cref="SyncUsersCommand"/>
internal class SyncUsersCommandHandler : AsyncRequestHandler<SyncUsersCommand>
{
    private const string DefaultUserRole = "User";
    private readonly ICrmAccessor crmAccessor;
    private readonly IMapper mapper;
    private readonly IAppDbContext appDbContext;
    private readonly IMediator mediator;
    private readonly RoleManager<AppIdentityRole> roleManager;
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SyncUsersCommandHandler(ICrmAccessor crmAccessor, IMapper mapper, IAppDbContext appDbContext,
        IMediator mediator, RoleManager<AppIdentityRole> roleManager, UserManager<User> userManager)
    {
        this.crmAccessor = crmAccessor;
        this.mapper = mapper;
        this.appDbContext = appDbContext;
        this.mediator = mediator;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    /// <inheritdoc/>
    protected override async Task Handle(SyncUsersCommand request, CancellationToken cancellationToken)
    {
        var crmUsersDto = (await crmAccessor.GetUsersAsync(cancellationToken)).Users;
        var crmUsers = mapper.Map<IEnumerable<User>>(crmUsersDto);

        var dbUsersDto = await mediator.Send(new GetAllUsersQuery() { IncludeDeleted = true }, cancellationToken);

        var dbUsers = mapper.Map<IEnumerable<User>>(dbUsersDto);

        var crmDbUsersDiff = GetDifferenceBetweenCrmUsersAndDbUsers(dbUsers, crmUsers);

        await RestoreUsersAsync(crmUsers, dbUsers, cancellationToken);

        if (!crmDbUsersDiff.HasDifferences)
        {
            return;
        }

        await UpdateUsersAsync(crmDbUsersDiff.Updated.Select(x=>x.Target), CancellationToken.None);

        await DeleteUsersAsync(crmDbUsersDiff.Removed, CancellationToken.None);

        await AddUsersAsync(crmDbUsersDiff.Added);

        await AddDefaultRolesToUsersAsync(crmDbUsersDiff.Added, crmUsersDto, CancellationToken.None);
    }

    private async Task AddDefaultRolesToUsersAsync(IEnumerable<User> users, IEnumerable<UserDto> crmUsersDto, CancellationToken cancellationToken)
    {
        var existingRoles = await roleManager.Roles.Where(role=>role.CrmRoleId != null).ToListAsync(cancellationToken);
        var existingRolesCrmIds = existingRoles.Select(role => role.CrmRoleId);

        foreach (var user in users)
        {
            var crmUser = crmUsersDto.First(userDto => userDto.Id == user.CrmId);

            var userRoles = crmUser.Roles;

            var rolesIntersection = existingRolesCrmIds.Intersect(userRoles).ToList();
            rolesIntersection = existingRoles.Where(role => rolesIntersection.Contains(role.CrmRoleId)).Select(role => role.Name).ToList();

            if (!rolesIntersection.Any())
            {
                var defaultRoleAdditionResult = await userManager.AddToRoleAsync(user, "User");
                if (!defaultRoleAdditionResult.Succeeded)
                {
                    throw new InfrastructureException(new Exception($"Failed to add default role for user {user}, with errors {string.Join(',', defaultRoleAdditionResult.Errors.Select(error => error.Description))}"));
                }
                continue;
            }

            var rolesAdditionResult = await userManager.AddToRolesAsync(user, rolesIntersection);

            if (!rolesAdditionResult.Succeeded)
            {
                throw new InfrastructureException(new Exception($"Failed to update roles for user {user}, with errors {string.Join(',', rolesAdditionResult.Errors.Select(error=>error.Description))}"));
            }
        }
    }

    private async Task RestoreUsersAsync(IEnumerable<User> crmUsers, IEnumerable<User> dbUsers, CancellationToken cancellationToken)
    {
        foreach (var crmUser in crmUsers)
        {
            if (dbUsers.Any(user=>user.CrmId == crmUser.CrmId && user.DeletedAt != null))
            {
                var user = await appDbContext.Users.GetAsync(dbUser => dbUser.CrmId == crmUser.CrmId, cancellationToken);
                user.DeletedAt = null;
            }
        }
        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    private DiffResult<User> GetDifferenceBetweenCrmUsersAndDbUsers(IEnumerable<User> dbUsers, IEnumerable<User> crmUsers)
    {
        var crmDbUsersDiff = CollectionUtils.Diff(dbUsers, crmUsers,
            (source, target) => source.CrmId == target.CrmId);
        return crmDbUsersDiff;
    }

    private async Task AddUsersAsync(IEnumerable<User> newUsers)
    {
        foreach (var newUser in newUsers)
        {
            await CreateUserAsync(newUser);
        }
    }

    private async Task CreateUserAsync(User user)
    {
        var createUserResult = await userManager.CreateAsync(user);

        if (!createUserResult.Succeeded)
        {
            throw new InfrastructureException(new Exception($"Failed to create user {user}, with errors {string.Join(',', createUserResult.Errors.Select(error => error.Description))}"));
        }
    }

    private async Task UpdateUsersAsync(IEnumerable<User> updatedUsers, CancellationToken cancellationToken)
    {
        foreach (var updatedUser in updatedUsers)
        {
            var dbUser = await appDbContext.Users.GetAsync(user => user.CrmId == updatedUser.CrmId, cancellationToken);
            dbUser.CopyFields(updatedUser);
        }
        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task DeleteUsersAsync(IEnumerable<User> deletedUsers, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteUserCommand(deletedUsers.Select(user => user.Id).ToArray()), cancellationToken);
    }
}
