namespace People.Web.ViewModels;

/// <summary>
/// Branches view model.
/// </summary>
public record BranchesViewModel
{
    /// <summary>
    /// Branches.
    /// </summary>
    public IEnumerable<BranchViewModel> Branches { get; init; }
}
