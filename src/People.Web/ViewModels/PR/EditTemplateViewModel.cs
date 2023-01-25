namespace People.Web.ViewModels.PR;

/// <summary>
/// Edit template view model.
/// </summary>
public record EditTemplateViewModel
{
    /// <inheritdoc cref="TemplateOptionsViewModel"/>
    public TemplateOptionsViewModel Options { get; init; }

    /// <inheritdoc cref="TemplateExtendedViewModel"/>
    public TemplateExtendedViewModel Template { get; init; }
}
