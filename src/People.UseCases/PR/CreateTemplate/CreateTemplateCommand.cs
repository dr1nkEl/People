using MediatR;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.CreateTemplate;

/// <summary>
/// Create perfomance review template command.
/// </summary>
/// <param name="Template">Perfomance review template.</param>
public record CreateTemplateCommand(NewTemplateDto Template) : IRequest;
