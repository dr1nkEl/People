using People.UseCases.Common.Dtos.Position;
using People.UseCases.Positions.Common.Dtos;

namespace People.Web.ViewModels.Position;

/// <summary>
/// Edit position view model record.
/// </summary>
public record EditPositionViewModel
{
    /// <summary>
    /// Position to edit.
    /// </summary>
    public PositionWithRelationsDto EditedPosition { get; set; } = new PositionWithRelationsDto();

    /// <summary>
    /// Positions.
    /// </summary>
    public IEnumerable<PositionDto> Positions { get; set; } = new List<PositionDto>();
}
