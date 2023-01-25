namespace People.Web.ViewModels.PR;

/// <summary>
/// Perfomance review template list view model.
/// </summary>
public record ListViewModel
{
    /// <summary>
    /// Templates.
    /// </summary>
    public IEnumerable<TemplateViewModel> Templates { get; init; }
}
