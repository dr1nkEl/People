using System.ComponentModel.DataAnnotations;
using People.Domain.Users.Entities;

namespace People.UseCases.Common.Dtos.Attribute;

/// <summary>
/// New attribute DTO.
/// </summary>
public record NewAttributeDto
{
    /// <inheritdoc cref="UserAttribute.Name"/>
    [Required]
    [MaxLength(255)]
    public string Name { get; init; }

    /// <summary>
    /// View roles IDs for <inheritdoc cref="UserAttribute.AllowViewRoles"/>.
    /// </summary>
    public IEnumerable<int> ViewRolesIds { get; init; }

    /// <summary>
    /// Edit roles IDs for <inheritdoc cref="UserAttribute.AllowEditRoles"/>.
    /// </summary>
    public IEnumerable<int> EditRolesIds { get; init; }

    /// <inheritdoc cref="UserAttribute.AttributeOptions"/>
    public IEnumerable<AttributeOptionDto> AttributeOptions { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowViewSelf"/>
    public bool AllowViewSelf { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowEditSelf"/>
    public bool AllowEditSelf { get; init; }

    /// <inheritdoc cref="UserAttribute.AttributeType"/>
    public AttributeType AttributeType { get; init; }
}
