using MediatR;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users;

/// <summary>
/// Get all users basic query.
/// </summary>
public record GetAllUsersQuery(int? BranchId = null, bool IncludeDeleted = false) : IRequest<IEnumerable<UserDto>>;
