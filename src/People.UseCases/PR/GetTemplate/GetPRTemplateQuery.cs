using MediatR;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.GetTemplate;

/// <summary>
/// Get perfomance review template query.
/// </summary>
/// <param name="Id">ID.</param>
public record GetPRTemplateQuery(int Id) : IRequest<PRTemplateDetailedDto>;
