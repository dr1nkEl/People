using System.Text.Json.Serialization;
using People.Domain.Users.Entities;

namespace People.Infrastructure.Common.Crm.Dto;

/// <inheritdoc cref="Branch"/>
public record BranchCrmDto
{
    /// <inheritdoc cref="Branch.CrmId"/>
    [JsonPropertyName("branchID")]
    public int CrmId { get; init; }

    /// <inheritdoc cref="Branch.Name"/>
    public string Name { get; init; }
}
