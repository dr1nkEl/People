using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.PR;

/// <summary>
/// Perfomance review template view model.
/// </summary>
public record TemplateViewModel
{
    /// <inheritdoc cref="ReviewTemplate.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="ReviewTemplate.Name"/>
    public string Name { get; init; }
}
