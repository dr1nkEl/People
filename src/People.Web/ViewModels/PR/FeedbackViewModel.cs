using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.PR;

/// <summary>
/// Feedback view model.
/// </summary>
public record FeedbackViewModel
{
    /// <summary>
    /// User under review.
    /// </summary>
    public Domain.Users.Entities.User ReviewedUser { get; init; }

    /// <summary>
    /// User which given feedback.
    /// </summary>
    public Domain.Users.Entities.User FeedbackUser { get; init; }

    /// <summary>
    /// Provided reply.
    /// </summary>
    public Reply Reply { get; init; }
}
