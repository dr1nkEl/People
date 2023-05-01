using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Common.Dtos.Branch;

/// <inheritdoc cref="Domain.Users.Entities.Branch"/>
public class BranchDto
{
    /// <inheritdoc cref="Domain.Users.Entities.Branch.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.Branch.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.Branch.DirectorId"/>
    public int? DirectorId { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.Branch.Director"/>
    public UserDto Director { get; init; }
}
