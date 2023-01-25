namespace People.UseCases.Common.Dtos.Position;

/// <summary>
/// Position DTO.
/// </summary>
public record PositionDto
{
    /// <inheritdoc cref="Domain.Users.Entities.Position.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.Position.Name"/>
    public string Name { get; init; }
}
