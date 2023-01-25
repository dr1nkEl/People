using MediatR;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.EFCore;

namespace People.UseCases.PR.DeleteTemplate;

/// Handler for <inheritdoc cref="DeleteTemplateCommand"/>
internal class DeleteTemplateCommandHandler : AsyncRequestHandler<DeleteTemplateCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">App DB context.</param>
    public DeleteTemplateCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
    {
        var item = await appDbContext.ReviewTemplates.GetAsync(template => template.Id == request.TemplateId, cancellationToken);
        item.DeletedAt = DateTime.UtcNow;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
