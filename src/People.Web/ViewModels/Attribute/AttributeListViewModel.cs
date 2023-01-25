namespace People.Web.ViewModels.Attribute;

/// <summary>
/// Attribute list view model.
/// </summary>
public record AttributeListViewModel
{
    /// <summary>
    /// Attributes.
    /// </summary>
    public IEnumerable<AttributeViewModel> Attributes { get; init; }
}
