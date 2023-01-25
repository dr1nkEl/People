using MediatR;
using People.UseCases.Common.Dtos.Attribute;

namespace People.UseCases.Administration.Attributes.UpdateUserAttribute;

/// <summary>
/// Update user attribute command.
/// </summary>
public record UpdateUserAttributeCommand(EditAttributeDto EditedAttribute) : IRequest;
