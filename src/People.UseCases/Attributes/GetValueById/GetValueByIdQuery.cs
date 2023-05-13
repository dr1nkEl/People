using MediatR;
using People.Domain.Users.Entities;

namespace People.UseCases.Attributes.GetValueById;

/// <summary>
/// Get value by id query.
/// </summary>
/// <param name="Id">Id.</param>
public record GetValueByIdQuery(int Id) : IRequest<AttributeValue>;
