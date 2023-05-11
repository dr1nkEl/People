using MediatR;
using People.Domain.Users.Entities;

namespace People.UseCases.Administration.GetRolesOfUser;

/// <summary>
/// Get roles of user query.
/// </summary>
public record GetRolesOfUserQuery(int UserId) : IRequest<IEnumerable<AppIdentityRole>>;
