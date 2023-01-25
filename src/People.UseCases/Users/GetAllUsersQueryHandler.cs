using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users;

/// <inheritdoc cref="GetAllUsersQuery"/>
internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAllUsersQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>.
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Users.AsQueryable();

        if (!request.IncludeDeleted)
        {
            query = query.Where(x => x.DeletedAt == null);
        }

        if (request.BranchId != null)
        {
            query = query.Where(x=>x.BranchId == request.BranchId);
        }

        return await mapper.ProjectTo<UserDto>(query).ToListAsync(cancellationToken);
    }
}
