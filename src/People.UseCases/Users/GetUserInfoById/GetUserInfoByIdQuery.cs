using MediatR;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users.GetUserInfo;

/// <summary>
/// Get user info by ID query.
/// </summary>
/// <param name="Id">User ID.</param>
public record GetUserInfoByIdQuery(int Id) : IRequest<UserInfoDto>;
