using People.UseCases.Common.Dtos.Administration;
using People.Web.ViewModels.Attribute;

namespace People.Web.ViewModels;

/// <summary>
/// Administration view model.
/// </summary>
public record RoleListViewModel
{
    /// <summary>
    /// Roles.
    /// </summary>
    public IEnumerable<RoleDto> Roles { get; init; }
}
