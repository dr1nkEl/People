using MediatR;
using People.UseCases.Common.Dtos.Attribute;

namespace People.UseCases.Administration.Attributes.CreateUserAttribute;

/// <summary>
/// Create user attribute command.
/// </summary>
public record CreateUserAttributeCommand(NewAttributeDto Attribute) : IRequest;
