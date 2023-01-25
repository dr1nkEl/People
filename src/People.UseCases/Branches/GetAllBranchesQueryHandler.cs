using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Branch;

namespace People.UseCases.Branches;

/// <inheritdoc cref="GetAllBranchesQuery"/>
internal class GetAllBranchesQueryHandler : IRequestHandler<GetAllBranchesQuery, IEnumerable<BranchDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAllBranchesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>.
    public async Task<IEnumerable<BranchDto>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Branches.AsQueryable();
        var branches = await mapper.ProjectTo<BranchDto>(query).ToListAsync(cancellationToken);
        return branches;
    }
}
