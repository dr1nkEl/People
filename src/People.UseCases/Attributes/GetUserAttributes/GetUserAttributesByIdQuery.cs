using MediatR;
using People.Domain.Users.Entities;

namespace People.UseCases.Attributes.GetUserAttributes;

/// <summary>
/// Get attributes of specified user query.
/// </summary>
/// <param name="UserId">Id of user.</param>
public record GetUserAttributesByIdQuery(int UserId) : IRequest<IEnumerable<UserAttribute>>;
