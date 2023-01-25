using People.UseCases.Positions.Common.Dtos;

namespace People.Web.ViewModels.Position;

/// <summary>
/// List position view model record.
/// </summary>
public record ListPositionViewModel
{
    /// <summary>
    /// Positions.
    /// </summary>
    public IEnumerable<PositionWithRelationsDto> Positions { get; set; } = new List<PositionWithRelationsDto>();
}
