using System.ComponentModel.DataAnnotations;
using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.PR;

/// <summary>
/// Extended template view model.
/// </summary>
public record TemplateExtendedViewModel
{
    /// <inheritdoc cref="ReviewTemplate.Id"/>
    public int? Id { get; init; }

    /// <inheritdoc cref="ReviewTemplate.Name"/>
    [Required]
    [MaxLength(255)]
    public string Name { get; init; }

    /// <inheritdoc cref="ReviewTemplate.ReviewedUserQuestions"/>
    public ICollection<QuestionViewModel> ReviewedUserQuestions { get; init; }

    /// <inheritdoc cref="ReviewTemplate.FeedbackQuestions"/>
    public ICollection<QuestionViewModel> FeedbackQuestions { get; init; }

    /// <inheritdoc cref="ReviewTemplate.RelatedPositionId"/>
    public int? RelatedPositionId { get; init; }

    /// <inheritdoc cref="ReviewTemplate.ReviewTypeId"/>
    [Required]
    public int? ReviewTypeId { get; init; }
}
