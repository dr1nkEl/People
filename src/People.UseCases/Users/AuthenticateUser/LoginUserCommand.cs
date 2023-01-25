using System.ComponentModel.DataAnnotations;
using MediatR;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users.AuthenticateUser;

/// <summary>
/// Login user command.
/// </summary>
public record LoginUserCommand : IRequest
{
    /// <summary>
    /// Email.
    /// </summary>
    [EmailAddress]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }

    /// <summary>
    /// Remember user's cookie for longer period.
    /// </summary>
    public bool RememberMe { get; init; }
}
