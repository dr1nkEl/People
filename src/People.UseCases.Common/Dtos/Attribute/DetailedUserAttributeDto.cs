using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.Administration;

namespace People.UseCases.Common.Dtos.Attribute;

/// <summary>
/// Detailed user attribute DTO.
/// </summary>
public record DetailedUserAttributeDto
{
    /// <inheritdoc cref="UserAttribute.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="UserAttribute.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="UserAttribute.AttributeType"/>
    public AttributeType AttributeType { get; init; }

    /// <inheritdoc cref="UserAttribute.AttributeOptions"/>
    public IEnumerable<AttributeOptionDto> AttributeOptions { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowEditRoles"/>
    public IEnumerable<RoleDto> AllowEditRoles { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowEditSelf"/>
    public bool AllowEditSelf { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowViewRoles"/>
    public IEnumerable<RoleDto> AllowViewRoles { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowViewSelf"/>
    public bool AllowViewSelf { get; init; }
}
