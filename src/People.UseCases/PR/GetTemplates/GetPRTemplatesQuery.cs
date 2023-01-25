using MediatR;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.GetTemplates;

/// <summary>
/// Get perfomance review templates query.
/// </summary>
public record GetPRTemplatesQuery : IRequest<IEnumerable<PRTemplateDto>>;
