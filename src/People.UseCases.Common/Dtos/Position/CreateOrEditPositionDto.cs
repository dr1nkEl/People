namespace People.UseCases.Common.Dtos.Position;

/// <summary>
/// Create or edit position DTO.
/// </summary>
public record CreateOrEditPositionDto
{
    /// <summary>
    /// Position Id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Position Name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Parent Id.
    /// </summary>
    public int ParentId { get; init; }

    /// <summary>
    /// Child position ids.
    /// </summary>
    public IEnumerable<int> ChildIds { get; init; } = new List<int>();
}
