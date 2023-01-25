using People.Domain.Users.Entities;

namespace People.UseCases.Common.Dtos.Attribute;

/// <summary>
/// Edit attribute DTO.
/// </summary>
public record EditAttributeDto
{
    /// <inheritdoc cref="UserAttribute.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="UserAttribute.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="UserAttribute.AttributeType"/>
    public AttributeType AttributeType { get; init; }

    /// <summary>
    /// New view roles IDs for <inheritdoc cref="UserAttribute.AllowViewRoles"/>.
    /// </summary>
    public IEnumerable<int> AllowViewRoles { get; init; }

    /// <summary>
    /// New edit roles IDs for <inheritdoc cref="UserAttribute.AllowEditRoles"/>.
    /// </summary>
    public IEnumerable<int> AllowEditRoles { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowViewSelf"/>
    public bool AllowViewSelf { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowEditSelf"/>
    public bool AllowEditSelf { get; init; }

    /// Titles of <inheritdoc cref="UserAttribute.AttributeOptions"/>
    public IEnumerable<AttributeOptionDto> AttributeOptions { get; init; }
}
