using MediatR;
using People.UseCases.Common.Dtos.Branch;

namespace People.UseCases.Branches;

/// <summary>
/// Get branch by Id query.
/// </summary>
public record GetBranchByIdQuery(int BranchId) : IRequest<BranchDto>;
