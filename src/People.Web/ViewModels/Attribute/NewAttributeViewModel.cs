using System.ComponentModel.DataAnnotations;
using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.Administration;

namespace People.Web.ViewModels.Attribute;

/// <summary>
/// New attribute view model.
/// </summary>
public record NewAttributeViewModel
{
    /// <inheritdoc cref="UserAttribute.Name"/>
    [Required]
    [MaxLength(255)]
    public string Name { get; init; }

    /// <summary>
    /// Roles.
    /// </summary>
    public IEnumerable<RoleDto> Roles { get; init; } = new List<RoleDto>();

    /// IDs for <inheritdoc cref="UserAttribute.AllowViewRoles"/>
    public IEnumerable<int> ViewRolesIds { get; init; } = new List<int>();

    /// Titles for <inheritdoc cref="UserAttribute.AttributeOptions"/>
    public IEnumerable<string> AttributeTitles { get; init; } = new List<string>();

    /// IDs for <inheritdoc cref="UserAttribute.AllowEditRoles"/>
    public IEnumerable<int> EditRolesIds { get; init; } = new List<int>();

    /// <inheritdoc cref="UserAttribute.AllowViewSelf"/>
    public bool AllowViewSelf { get; init; }

    /// <inheritdoc cref="UserAttribute.AllowEditSelf"/>
    public bool AllowEditSelf { get; init; }

    /// <inheritdoc cref="UserAttribute.AttributeType"/>
    public AttributeType AttributeType { get; init; }
}
