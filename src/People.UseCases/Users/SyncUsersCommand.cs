using MediatR;

namespace People.UseCases.Users;

/// <summary>
/// Sync users command.
/// </summary>
public record SyncUsersCommand : IRequest;
