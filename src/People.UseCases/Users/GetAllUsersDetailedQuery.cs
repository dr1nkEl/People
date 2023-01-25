using MediatR;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users;

/// <summary>
/// Get all users detailed query.
/// </summary>
public record GetAllUsersDetailedQuery(int? BranchId = null, bool IncludeDeleted = false) : IRequest<IEnumerable<DetailedUserDto>>;
