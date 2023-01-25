using MediatR;
using People.UseCases.Common.Dtos.Branch;

namespace People.UseCases.Branches;

/// <summary>
/// Get all branches query.
/// </summary>
public record GetAllBranchesQuery : IRequest<IEnumerable<BranchDto>>;
