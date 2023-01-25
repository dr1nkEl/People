namespace People.UseCases.Common.Dtos.User;

/// <summary>
/// User short information DTO.
/// </summary>
public record UserShortInfoDto
{
    /// <summary>
    /// Person`s ID.
    /// </summary>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.FullName"/>
    public string FullName { get; init; }

    /// <summary>
    /// Person`s Avatar URL.
    /// </summary>
    public string AvatarUrl { get; init; }
}
