using People.UseCases.Common.Dtos.Position;

namespace People.UseCases.Common.Dtos.User;

/// <summary>
/// Detailed user DTO.
/// </summary>
/// <inheritdoc cref="Domain.Users.Entities.User"/>
public record DetailedUserDto
{
    /// <summary>
    ///  ID.
    /// </summary>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.FirstName"/>
    public string FirstName { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.LastName"/>
    public string LastName { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.BranchId"/>
    public int BranchId { get; init; }

    /// Email.
    public string Email { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.Birthday"/>
    public DateOnly BirthDay { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.Positions"/>
    public IEnumerable<PositionDto> Positions { get; init; }
}
