using MediatR;

namespace People.UseCases.Administration.Attributes.DeleteUserAttribute;

/// <summary>
/// Delete attribute command.
/// </summary>
/// <param name="AttributeId">Attribute ID.</param>
public record DeleteAttributeCommand(int AttributeId) : IRequest;
