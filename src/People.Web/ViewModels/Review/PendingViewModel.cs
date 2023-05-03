using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.Review;

/// <summary>
/// Pending reviews view model.
/// </summary>
public record PendingViewModel
{
    /// <summary>
    /// Reviews which user should give answers for.
    /// </summary>
    public IEnumerable<PerformanceReview> ReviewsForUser { get; init; }

    /// <summary>
    /// Reviews for user itself.
    /// </summary>
    public IEnumerable<PerformanceReview> UserReviews { get; init; }
}
