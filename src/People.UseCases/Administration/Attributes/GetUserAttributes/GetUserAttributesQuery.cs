using MediatR;
using People.UseCases.Common.Dtos.Attribute;

namespace People.UseCases.Administration.Attributes.GetUserAttributes;

/// <summary>
/// Get existing user attributes query.
/// </summary>
public record GetUserAttributesQuery(bool IncludeDeleted = false) : IRequest<IEnumerable<UserAttributeDto>>;
