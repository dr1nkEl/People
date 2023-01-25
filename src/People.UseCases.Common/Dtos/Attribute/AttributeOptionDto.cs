using People.Domain.Users.Entities;

namespace People.UseCases.Common.Dtos.Attribute;

/// <inheritdoc cref="Domain.Users.Entities.AttributeOption"/>
public record AttributeOptionDto
{
    /// <inheritdoc cref="AttributeOption.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="AttributeOption.Title"/>
    public string Title { get; init; }
}
