using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users.GetUsersShortInfo;

/// <summary>
/// Get users short information.
/// </summary>
internal class GetUsersShortInfoQueryHandler : IRequestHandler<GetUsersShortInfoQuery, IEnumerable<UserShortInfoDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetUsersShortInfoQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserShortInfoDto>> Handle(GetUsersShortInfoQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Users.Where(user => user.DeletedAt == null);
        var users = await mapper.ProjectTo<UserShortInfoDto>(query)
            .ToListAsync(cancellationToken);
        return users;
    }
}
