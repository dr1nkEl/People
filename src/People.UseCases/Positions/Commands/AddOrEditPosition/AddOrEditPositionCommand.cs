using MediatR;
using People.UseCases.Common.Dtos.Position;

namespace People.UseCases.Positions.Commands;

/// <summary>
/// Add or edit position command.
/// </summary>
public record AddOrEditPositionCommand(CreateOrEditPositionDto Position) : IRequest;
