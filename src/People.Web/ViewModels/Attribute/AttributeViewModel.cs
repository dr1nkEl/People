namespace People.Web.ViewModels.Attribute;

/// <summary>
/// User attribute view model.
/// </summary>
public record AttributeViewModel
{
    /// <inheritdoc cref="Domain.Users.Entities.UserAttribute.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.UserAttribute.Name"/>
    public string Name { get; init; }
}
