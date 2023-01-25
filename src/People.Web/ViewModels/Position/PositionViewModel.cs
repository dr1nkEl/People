namespace People.Web.ViewModels.Position;

/// <summary>
/// Position view model.
/// </summary>
public record PositionViewModel
{
    /// <summary>
    /// ID of the position.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Position name.
    /// </summary>
    public string Name { get; init; }
}
