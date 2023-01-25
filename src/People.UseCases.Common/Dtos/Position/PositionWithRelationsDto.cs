using People.UseCases.Common.Dtos.Position;

namespace People.UseCases.Positions.Common.Dtos;

/// <summary>
/// Position with relations DTO.
/// </summary>
public record PositionWithRelationsDto
{
    /// <inheritdoc cref="Domain.Users.Entities.Position.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.Position.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.Position.ChildPositions"/>
    public IEnumerable<PositionDto> ChildPositions { get; init; } = new List<PositionDto>();

    /// <inheritdoc cref="Domain.Users.Entities.Position.ParentPosition"/>
    public PositionDto ParentPosition { get; init; }
}
