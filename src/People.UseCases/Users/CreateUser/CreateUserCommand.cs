using MediatR;

namespace People.UseCases.Users.CreateUser;

/// <summary>
/// Create user command.
/// </summary>
/// <param name="BirthDate">Birth date.</param>
/// <param name="Email">Email.</param>
/// <param name="FirstName">First name.</param>
/// <param name="LastName">Last name.</param>
/// <param name="Password">Password.</param>
/// <param name="branchId">Id of user branch.</param>
public record CreateUserCommand(string Email, string FirstName, string LastName, DateOnly BirthDate, string Password, int BranchId) : IRequest<int>;
