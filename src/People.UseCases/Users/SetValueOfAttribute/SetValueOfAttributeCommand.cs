using MediatR;

namespace People.UseCases.Users.SetValueOfAttribute;

/// <summary>
/// Set value of existing user attribute.
/// </summary>
/// <param name="Value">Value to be set.</param>
/// <param name="ValueId">Id of Attribute Value.</param>
public record SetValueOfAttributeCommand(int ValueId, string Value) : IRequest;
