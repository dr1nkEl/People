using MediatR;

namespace People.UseCases.Positions.Commands;

/// <summary>
/// Command to delete position by its name.
/// </summary>
public record DeletePositionCommand(int Id) : IRequest;
