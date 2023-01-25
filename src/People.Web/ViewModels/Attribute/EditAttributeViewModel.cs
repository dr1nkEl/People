using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.Administration;

namespace People.Web.ViewModels.Attribute;

/// <summary>
/// Edit attribute view model.
/// </summary>
public record EditAttributeViewModel
{
    /// <summary>
    /// Roles.
    /// </summary>
    public IEnumerable<RoleDto> Roles { get; init; }

    /// <inheritdoc cref="UserAttribute"/>
    public DetailedAttributeViewModel Attribute { get; init; }

    /// IDs for <inheritdoc cref="UserAttribute.AllowViewRoles"/>
    public IEnumerable<int> AllowViewRoles { get; init; }

    /// IDs for <inheritdoc cref="UserAttribute.AllowEditRoles"/>
    public IEnumerable<int> AllowEditRoles { get; init; }

    /// Titles for <inheritdoc cref="UserAttribute.AttributeOptions"/>
    public IEnumerable<string> AttributeTitles { get; init; }
}
