using MediatR;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Positions.Commands.Handlers;

/// <summary>
/// Delete position command handler.
/// </summary>
internal class DeletePositionCommandHandler : AsyncRequestHandler<DeletePositionCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public DeletePositionCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await appDbContext.Positions.GetAsync(pos => pos.Id == request.Id, cancellationToken);
        appDbContext.Positions.Remove(position);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
