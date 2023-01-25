using MediatR;

namespace People.UseCases.Branches;

/// <summary>
/// Update branch director command.
/// </summary>
public record UpdateBranchDirectorCommand(int BranchId, int? DirectorId) : IRequest;
