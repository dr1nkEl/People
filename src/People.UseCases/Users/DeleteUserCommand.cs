using MediatR;

namespace People.UseCases.Users;

/// <summary>
/// Delete user command.
/// </summary>
public record DeleteUserCommand(params int[] UserIds) : IRequest;
