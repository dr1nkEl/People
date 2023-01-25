using People.Domain.Reviews.Entities;

namespace People.Web.ViewModels.PR;

/// <summary>
/// Review type view model.
/// </summary>
public record ReviewTypeViewModel
{
    /// <inheritdoc cref="ReviewType.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="ReviewType.Name"/>
    public string Name { get; init; }
}
