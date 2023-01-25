using MediatR;

namespace People.UseCases.Users.AddUserToPosition;

/// <summary>
/// Edit user positions command.
/// </summary>
/// <param name="PositionIds">Position IDs.</param>
/// <param name="UserId">User ID.</param>
public record EditUserPositionsCommand(IEnumerable<int> PositionIds, int UserId) : IRequest;
