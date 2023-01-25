using System.ComponentModel.DataAnnotations;
using MediatR;

namespace People.UseCases.Users.AuthenticateUser;

/// <summary>
/// Refresh token command.
/// </summary>
public record RefreshTokenCommand : IRequest<TokenModel>
{
    /// <summary>
    /// User token.
    /// </summary>
    [Required]
    public string Token { get; init; }
}
