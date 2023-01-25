using People.UseCases.Common.Dtos.Administration;
using People.UseCases.Common.Dtos.Branch;
using People.UseCases.Common.Dtos.Position;
using People.UseCases.Common.Dtos.User;

namespace People.Web.ViewModels.User;

/// <summary>
/// User info view model.
/// </summary>
public record UserInfoViewModel
{
    /// <inheritdoc cref="UserInfoDto"/>
    public UserInfoDto User { get; init; }

    /// <inheritdoc cref="BranchNameDto"/>
    public BranchNameDto Branch { get; init; }

    /// <inheritdoc cref="PositionDto"/>
    public IEnumerable<PositionDto> Positions { get; set; }

    /// <inheritdoc cref="UserShortInfoDto"/>
    public IEnumerable<UserShortInfoDto> Users { get; set; }

    /// <summary>
    /// All roles.
    /// </summary>
    public IEnumerable<RoleDto> AllRoles { get; init; }
}
