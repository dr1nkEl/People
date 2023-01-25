using MediatR;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Administration.Attributes.DeleteUserAttribute;

/// <inheritdoc cref="DeleteAttributeCommand"/>
internal class DeleteAttributeCommandHandler : AsyncRequestHandler<DeleteAttributeCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeleteAttributeCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(DeleteAttributeCommand request, CancellationToken cancellationToken)
    {
        var attribute = await appDbContext.Attributes.GetAsync(request.AttributeId);
        attribute.DeletedAt = DateTime.UtcNow;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
