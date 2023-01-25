using People.Domain.Users.Entities;

namespace People.Web.ViewModels.Attribute;

/// <inheritdoc cref="AttributeOption"/>
public record AttributeOptionViewModel
{
    /// <inheritdoc cref="AttributeOption.Id"/>
    public int Id { get; set; }

    /// <inheritdoc cref="AttributeOption.Title"/>
    public string Title { get; set; }

    /// <inheritdoc cref="AttributeOption.AttributeId"/>
    public int AttributeId { get; set; }
}
