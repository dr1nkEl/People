using People.Domain.Reviews.Entities;

namespace People.UseCases.Common.Dtos.PR;

/// <summary>
/// Review type DTO.
/// </summary>
public record ReviewTypeDto
{
    /// <inheritdoc cref="ReviewType.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="ReviewType.Name"/>
    public string Name { get; init; }
}
