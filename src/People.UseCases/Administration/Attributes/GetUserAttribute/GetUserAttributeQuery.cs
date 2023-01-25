using MediatR;
using People.UseCases.Common.Dtos.Attribute;

namespace People.UseCases.Administration.Attributes.GetUserAttribute;

/// <summary>
/// Get user attribute query.
/// </summary>
public record GetUserAttributeQuery(int Id) : IRequest<DetailedUserAttributeDto>;
