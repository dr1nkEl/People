using MediatR;
using People.UseCases.Common.Dtos.Attribute;

namespace People.UseCases.Administration.Attributes.GetAttributeOptions;

/// <summary>
/// Get attribute options query.
/// </summary>
/// <param name="AttributeId">Attribute ID.</param>
public record GetAttributeOptionsQuery(int AttributeId) : IRequest<IEnumerable<AttributeOptionDto>>;
