using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Branch;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Branches;

/// <inheritdoc cref="GetBranchByIdQuery"/>
internal class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, BranchDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetBranchByIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>.
    public async Task<BranchDto> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Branches.AsQueryable();

        var branchDto = await mapper.ProjectTo<BranchDto>(query).GetAsync(x=>x.Id == request.BranchId, cancellationToken);

        return branchDto;
    }
}
