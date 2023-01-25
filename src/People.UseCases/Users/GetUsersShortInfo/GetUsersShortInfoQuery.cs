using MediatR;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users.GetUsersShortInfo;

/// <summary>
/// Get users short information query.
/// </summary>
public record GetUsersShortInfoQuery : IRequest<IEnumerable<UserShortInfoDto>>;
