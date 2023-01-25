using System.ComponentModel.DataAnnotations;
using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.Administration;

namespace People.Web.ViewModels.Attribute;

/// <summary>
/// Detailed user attribute view model.
/// </summary>
public record DetailedAttributeViewModel
{
    /// <inheritdoc cref="UserAttribute.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="UserAttribute.Name"/>
    [Required]
    public string Name { get; init; }

    /// <inheritdoc cref="UserAttribute.AttributeType"/>
    public AttributeType AttributeType { get; init; }

    /// <inheritdoc cref="UserAttribute.AttributeOptions"/>
    public IEnumerable<AttributeOptionViewModel> AttributeOptions { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowEditRoles"/>
    public IEnumerable<RoleDto> AllowEditRoles { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowEditSelf"/>
    public bool AllowEditSelf { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowViewRoles"/>
    public IEnumerable<RoleDto> AllowViewRoles { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowViewSelf"/>
    public bool AllowViewSelf { get; init; }
}
