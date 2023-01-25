using MediatR;

namespace People.UseCases.Users.AddReportingUsers;

/// <summary>
/// Add reporting users command.
/// </summary>
/// <param name="UserId">User ID.</param>
/// <param name="ReportingUserIds">Reporting user IDs.</param>
public record EditReportingUsersCommand(int UserId, IEnumerable<int> ReportingUserIds) : IRequest;
