using MediatR;

namespace People.UseCases.Users.ChangePasswordCommand;

/// <summary>
/// Change password command.
/// </summary>
/// <param name="NewPassword">New password of user.</param>
/// <param name="OldPassword">Old password of user.</param>
/// <param name="UserId">Id of user to change password for.</param>
public record ChangePasswordCommand(int UserId, string OldPassword, string NewPassword) : IRequest;
