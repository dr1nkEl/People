using MediatR;
using People.UseCases.Common.Dtos.Position;

namespace People.UseCases.Positions.Queries.GetPositions;

/// <summary>
/// Query to get positions.
/// </summary>
public record GetPositionsQuery : IRequest<IEnumerable<PositionDto>>;
