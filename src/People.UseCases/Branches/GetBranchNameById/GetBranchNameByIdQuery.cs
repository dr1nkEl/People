using MediatR;
using People.UseCases.Common.Dtos.Branch;

namespace People.UseCases.Branches.GetBranchNameById;

/// <summary>
/// Get branch name by id query.
/// </summary>
/// <param name="Id">Branch ID.</param>
public record GetBranchNameByIdQuery(int Id) : IRequest<BranchNameDto>;
