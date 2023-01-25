namespace People.Web.ViewModels.User;

/// <summary>
/// User list view model.
/// </summary>
public record UserListViewModel
{
    /// <summary>
    /// Branches.
    /// </summary>
    public IEnumerable<BranchViewModel> Branches { get; init; }
}
