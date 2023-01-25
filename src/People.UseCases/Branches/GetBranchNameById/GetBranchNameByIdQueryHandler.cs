using AutoMapper;
using MediatR;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Branch;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Branches.GetBranchNameById;

/// <summary>
/// Get branch name by id query handler.
/// </summary>
internal class GetBranchNameByIdQueryHandler : IRequestHandler<GetBranchNameByIdQuery, BranchNameDto>
{
    private readonly IMapper mapper;
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetBranchNameByIdQueryHandler(IMapper mapper, IAppDbContext appDbContext)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>.
    public async Task<BranchNameDto> Handle(GetBranchNameByIdQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Branches.AsQueryable();
        var branchDto = await mapper.ProjectTo<BranchNameDto>(query).GetAsync(branch => branch.Id == request.Id);
        return branchDto;
    }
}
