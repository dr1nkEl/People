using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users;

/// <inheritdoc cref="GetAllUsersDetailedQuery"/>
internal class GetAllUsersDetailedQueryHandler : IRequestHandler<GetAllUsersDetailedQuery, IEnumerable<DetailedUserDto>>
{
    private readonly IMediator mediator;
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAllUsersDetailedQueryHandler(IMediator mediator, IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<DetailedUserDto>> Handle(GetAllUsersDetailedQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Users.AsQueryable();

        if (!request.IncludeDeleted)
        {
            query = query.Where(x => x.DeletedAt == null);
        }

        if (request.BranchId != null)
        {
            query = query.Where(x => x.BranchId == request.BranchId);
        }

        var users = await mapper.ProjectTo<DetailedUserDto>(query).ToListAsync(cancellationToken);

        return users;
    }
}
