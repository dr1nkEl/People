namespace People.UseCases.Common.Dtos.Branch;

/// <summary>
/// Branch name DTO.
/// </summary>
public record BranchNameDto
{
    /// <inheritdoc cref="Domain.Users.Entities.Branch.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.Branch.Name"/>
    public string Name { get; init; }
}
