using People.Domain.Reviews.Entities;

namespace People.UseCases.Common.Dtos.PR;

/// <summary>
/// Perfomance review template DTO.
/// </summary>
public record PRTemplateDto
{
    /// <inheritdoc cref="ReviewTemplate.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="ReviewTemplate.Name"/>
    public string Name { get; init; }
}
