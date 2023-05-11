using People.Domain.Reviews.Entities;
using People.Domain.Users.Entities;
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
    /// Reviews which user should give answers for.
    /// </summary>
    public IEnumerable<PerformanceReview> UserReviews { get; set; }

    /// <summary>
    /// Reviews for user itself.
    /// </summary>
    public IEnumerable<PerformanceReview> ReviewsForUser { get; set; }

    /// <summary>
    /// All roles.
    /// </summary>
    public IEnumerable<RoleDto> AllRoles { get; init; }

    /// <summary>
    /// Roles with permissions of current user.
    /// </summary>
    public IEnumerable<AppIdentityRole> RolesOfUser { get; init; }

    /// <summary>
    /// Attributes of user whom page belongs to.
    /// </summary>
    public IEnumerable<UserAttribute> AttributesOfUser { get; init; }

    /// <summary>
    /// All attributes in system.
    /// </summary>
    public IEnumerable<UserAttribute> AllAttributes { get; init; }

    /// <summary>
    /// Attributes from modal to set to user.
    /// </summary>
    public IEnumerable<int> AttributeIdsToSet { get; init; }
}
