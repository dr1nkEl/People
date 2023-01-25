using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Positions.Common.Dtos;

namespace People.UseCases.Positions.Queries.GetPositionsWithInfo;

/// <summary>
/// Get position query handler class.
/// </summary>
internal class GetPositionsWithInfoQueryHandler : IRequestHandler<GetPositionsWithInfoQuery, IEnumerable<PositionWithRelationsDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public GetPositionsWithInfoQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PositionWithRelationsDto>> Handle(GetPositionsWithInfoQuery request, CancellationToken cancellationToken)
    {
        var positionsDto = await mapper.ProjectTo<PositionWithRelationsDto>(appDbContext.Positions).ToListAsync(cancellationToken);
        return positionsDto;
    }
}
