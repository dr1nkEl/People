using System.Text.Json.Serialization;

namespace People.Infrastructure.Common.Crm.Dto;

/// <summary>
/// Branches DTO.
/// </summary>
public record BranchesDto
{
    /// <summary>
    /// Branches.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<BranchCrmDto> Branches { get; init; }
}
