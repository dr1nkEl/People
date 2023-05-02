using MediatR;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.GetTemplatesForUser;

/// <summary>
/// Get perfomance review templates for user query.
/// </summary>
public record GetPrTemplatesForUserQuery(int UserId) : IRequest<IEnumerable<PRTemplateDto>>;
