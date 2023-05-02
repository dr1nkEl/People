using System.ComponentModel.DataAnnotations;
using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.PR;

/// <summary>
/// Set review for user view model page.
/// </summary>
public record SetViewModel
{
    /// <summary>
    /// Array of <see cref="ReviewTemplates"/>.
    /// </summary>
    public IEnumerable<TemplateViewModel> ReviewTemplates { get; init; } = new List<TemplateViewModel>();

    /// <summary>
    /// Array of <see cref="UserDetailedViewModel"/>.
    /// </summary>
    public IEnumerable<UserDetailedViewModel> Users { get; init; } = new List<UserDetailedViewModel>();

    /// <summary>
    /// Id of User.
    /// </summary>
    public int? ReviewedUserId { get; init; }

    /// <summary>
    /// Reviewed by following users.
    /// </summary>
    public IEnumerable<int> ReviewedByUsersIds { get; set; } = new List<int>();

    /// <summary>
    /// Deadline of review.
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// Id of template.
    /// </summary>
    public int? TemplateId { get; set; }
}
