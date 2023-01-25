using People.UseCases.Common.Dtos.Position;

namespace People.Web.ViewModels.Position;

/// <summary>
/// Create position view model.
/// </summary>
public record CreatePositionViewModel
{
    /// <summary>
    /// Positions.
    /// </summary>
    public IEnumerable<PositionDto> Positions { get; set; } = new List<PositionDto>();
}
