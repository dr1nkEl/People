using MediatR;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users.GetUserById;

/// <summary>
/// Get user detailed by identifier.
/// </summary>
public record GetUserByIdQuery : IRequest<UserDto>
{
    /// <summary>
    /// User id.
    /// </summary>
    public int UserId { get; init; }
}
