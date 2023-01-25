using People.Web.ViewModels.Position;

namespace People.Web.ViewModels.PR;

/// <summary>
/// Template options view model.
/// </summary>
public record TemplateOptionsViewModel
{
    /// <summary>
    /// Review types.
    /// </summary>
    public IEnumerable<ReviewTypeViewModel> ReviewTypes { get; init; }

    /// <summary>
    /// Positions.
    /// </summary>
    public IEnumerable<PositionViewModel> Positions { get; init; }

    /// <summary>
    /// Existing templates.
    /// </summary>
    public IEnumerable<TemplateViewModel> Templates { get; init; }
}
