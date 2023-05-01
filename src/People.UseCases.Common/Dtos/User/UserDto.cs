namespace People.UseCases.Common.Dtos.User;

/// <inheritdoc cref="Domain.Users.Entities.User"/>
public record UserDto
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

    /// <inheritdoc cref="Domain.Users.Entities.User.DeletedAt"/>
    public DateTime? DeletedAt { get; init; }

    /// Email.
    public string Email { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.Birthday"/>
    public DateOnly BirthDay { get; init; }
}
