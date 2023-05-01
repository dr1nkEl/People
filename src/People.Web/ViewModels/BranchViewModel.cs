using People.Domain.Users.Entities;
using People.Web.ViewModels.User;

namespace People.Web.ViewModels;

/// <inheritdoc cref="Branch"/>
public record BranchViewModel
{
    /// <inheritdoc cref="Branch.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="Branch.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="Branch.DirectorId"/>
    public int? DirectorId { get; init; }

    /// <inheritdoc cref="Branch.Director"/>
    public UserViewModel Director { get; init; }
}
