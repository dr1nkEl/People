using System.ComponentModel.DataAnnotations;
using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.PR;

/// <summary>
/// New review type view model.
/// </summary>
public record NewReviewTypeViewModel
{
    /// <inheritdoc cref="ReviewType.Name"/>
    [Required]
    [MaxLength(255)]
    public string Name { get; init; }

    /// <inheritdoc cref="ReviewType.Interval"/>
    public Interval? Interval { get; init; }

    /// <summary>
    /// Interval amount.
    /// </summary>
    public int? IntervalAmount { get; init; }
}
