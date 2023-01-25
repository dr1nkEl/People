using People.Web.ViewModels.User;

namespace People.Web.ViewModels;

/// <summary>
/// Branch edit view model.
/// </summary>
public record BranchEditViewModel
{
    /// <summary>
    /// Branch.
    /// </summary>
    public BranchViewModel Branch { get; init; }

    /// <summary>
    /// Users.
    /// </summary>
    public IEnumerable<UserViewModel> Users { get; init; }
}
