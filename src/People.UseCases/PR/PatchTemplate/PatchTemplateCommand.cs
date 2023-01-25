using MediatR;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.PatchTemplate;

/// <summary>
/// Patch template command.
/// </summary>
/// <param name="Template">Template.</param>
public record PatchTemplateCommand(PRTemplateDetailedDto Template) : IRequest;
