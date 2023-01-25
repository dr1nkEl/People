using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Position;

namespace People.UseCases.Positions.Queries.GetPositions;

/// <summary>
/// Get position query handler class.
/// </summary>
internal class GetPositionsQueryHandler : IRequestHandler<GetPositionsQuery, IEnumerable<PositionDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public GetPositionsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PositionDto>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
    {
        var positionsDto = await mapper.ProjectTo<PositionDto>(appDbContext.Positions).ToListAsync(cancellationToken);
        return positionsDto;
    }
}
