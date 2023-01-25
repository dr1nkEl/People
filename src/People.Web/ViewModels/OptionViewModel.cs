namespace People.Web.ViewModels;

/// <summary>
/// Option view model.
/// </summary>
public record OptionViewModel
{
    /// <summary>
    /// Text.
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Value.
    /// </summary>
    public int Value { get; init; }
}
