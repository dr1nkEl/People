using People.UseCases.Common.Dtos.Position;

namespace People.UseCases.Common.Dtos.User;

/// <summary>
/// User info DTO.
/// </summary>
public record UserInfoDto
{
    /// <summary>
    /// Person`s ID.
    /// </summary>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.FullName"/>
    public string FullName { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.BranchId"/>
    public int BranchId { get; init; }

    /// <summary>
    /// Person`s avatar URL.
    /// </summary>
    public string AvatarUrl { get; init; }

    /// <summary>
    /// Person`s email.
    /// </summary>
    public string Email { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.Birthday"/>
    public DateOnly BirthDay { get; init; }

    /// <summary>
    /// Person`s roles.
    /// </summary>
    public IList<string> Roles { get; init; } = new List<string>();

    /// <summary>
    /// Reporting users.
    /// </summary>
    public IList<UserShortInfoDto> ReportingUsers { get; init; } = new List<UserShortInfoDto>();

    /// <summary>
    /// Person`s positions.
    /// </summary>
    public IEnumerable<PositionDto> Positions { get; init; }
}
