using System.Text.Json.Serialization;

namespace People.Infrastructure.Common.Crm.Dto;

/// <summary>
/// Users DTO.
/// </summary>
public record UsersDto
{
    /// <summary>
    /// Users.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<UserDto> Users { get; init; }
}
