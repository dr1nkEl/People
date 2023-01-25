using System.Text.Json.Serialization;
using People.Domain.Users.Entities;

namespace People.Infrastructure.Common.Crm.Dto;

/// <summary>
/// User DTO.
/// </summary>
public record UserDto
{
    /// <summary>
    /// User identifier.
    /// </summary>
    [JsonPropertyName("userId")]
    public int Id { get; init; }

    /// <inheritdoc cref="User.FirstName"/>
    public string FirstName { get; init; }

    /// <inheritdoc cref="User.LastName"/>
    public string LastName { get; init; }

    /// <inheritdoc cref="User.BranchId"/>
    public int BranchId { get; init; }

    /// <inheritdoc cref="User.Birthday"/>
    [JsonPropertyName("birthdate")]
    public DateTime? BirthDay { get; init; }

    /// <summary>
    /// Email.
    /// </summary>
    [JsonPropertyName("primaryEmail")]
    public string Email { get; init; }

    /// <summary>
    /// Roles.
    /// </summary>
    public IEnumerable<string> Roles { get; init; }
}
