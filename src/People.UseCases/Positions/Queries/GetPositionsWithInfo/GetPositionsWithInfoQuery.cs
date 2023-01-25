using MediatR;
using People.UseCases.Positions.Common.Dtos;

namespace People.UseCases.Positions.Queries.GetPositionsWithInfo;

/// <summary>
/// Query to get positions with info about child and parent positions.
/// </summary>
public record GetPositionsWithInfoQuery : IRequest<IEnumerable<PositionWithRelationsDto>>;
