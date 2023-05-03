using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.Review;

/// <summary>
/// Process PR view model.
/// </summary>
public record ProcessViewModel
{
    /// <summary>
    /// <see cref="PerformanceReview"/>.
    /// </summary>
    public PerformanceReview Review { get; set; }

    /// <summary>
    /// <see cref="Reply"/>.
    /// </summary>
    public Reply Reply { get; set; }

    /// <summary>
    /// Is current pr for current user.
    /// </summary>
    public bool IsPrForCurrentUser { get; init; }
}
