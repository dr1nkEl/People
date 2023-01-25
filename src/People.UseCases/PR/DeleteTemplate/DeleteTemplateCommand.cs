using MediatR;

namespace People.UseCases.PR.DeleteTemplate;

/// <summary>
/// Delete template command.
/// </summary>
/// <param name="TemplateId">Template ID.</param>
public record DeleteTemplateCommand(int TemplateId) : IRequest;
