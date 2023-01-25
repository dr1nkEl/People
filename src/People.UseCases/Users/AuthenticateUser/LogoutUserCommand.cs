using MediatR;

namespace People.UseCases.Users.AuthenticateUser;

/// <summary>
/// Command for logout user.
/// </summary>
public record LogoutUserCommand : IRequest;
