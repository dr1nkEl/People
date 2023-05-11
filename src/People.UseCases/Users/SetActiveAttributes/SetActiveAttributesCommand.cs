using MediatR;

namespace People.UseCases.Users.SetActiveAttributes;

/// <summary>
/// Command to set active attributes to user by attribute's ids.
/// </summary>
/// <param name="UserId">Id of user.</param>
/// <param name="AttributeIds">IDs of attributes to be set to user.</param>
public record SetActiveAttributesCommand(int UserId, IEnumerable<int> AttributeIds) : IRequest;
