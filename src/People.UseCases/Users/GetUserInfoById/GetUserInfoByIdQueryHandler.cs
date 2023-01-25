using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.User;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Users.GetUserInfo;

/// <summary>
/// Get user info by id query handler.
/// </summary>
public class GetUserInfoByIdQueryHandler : IRequestHandler<GetUserInfoByIdQuery, UserInfoDto>
{
    private readonly IMapper mapper;
    private readonly IAppDbContext appDbContext;
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetUserInfoByIdQueryHandler(IMapper mapper, IAppDbContext appDbContext, UserManager<User> userManager)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
        this.userManager = userManager;
    }

    /// <inheritdoc/>.
    public async Task<UserInfoDto> Handle(GetUserInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Users.Include(usr => usr.Positions);
        var user = await query.GetAsync(usr => usr.Id == request.Id, cancellationToken);
        var userDto = mapper.Map<UserInfoDto>(user);
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            userDto.Roles.Add(role);
        }

        var reportingUsers = await GetReportingUsersAsync(request.Id, cancellationToken);
        foreach (var reportingUser in reportingUsers)
        {
            userDto.ReportingUsers.Add(reportingUser);
        }

        return userDto;
    }

    private async Task<IEnumerable<UserShortInfoDto>> GetReportingUsersAsync(int id, CancellationToken cancellationToken)
    {
        var reportingUsers = await appDbContext.UserPositionHierarchies.Where(hierarchy => hierarchy.ParentUserId == id)
            .Select(user => user.ChildUser)
            .ToListAsync(cancellationToken);
        var reportingUsersDto = mapper.Map<IEnumerable<UserShortInfoDto>>(reportingUsers);
        return reportingUsersDto;
    }
}
