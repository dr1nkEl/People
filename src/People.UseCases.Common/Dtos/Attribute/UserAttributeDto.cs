using People.Domain.Users.Entities;

namespace People.UseCases.Common.Dtos.Attribute;

/// <summary>
/// User attribute view model.
/// </summary>
public record UserAttributeDto
{
    /// <inheritdoc cref="UserAttribute.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="UserAttribute.Name"/>
    public string Name { get; init; }
}
