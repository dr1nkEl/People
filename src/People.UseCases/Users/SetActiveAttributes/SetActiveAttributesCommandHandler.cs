using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.Users.SetActiveAttributes;

/// <summary>
/// Handler for <see cref="SetActiveAttributesCommand"/>.
/// </summary>
internal class SetActiveAttributesCommandHandler : AsyncRequestHandler<SetActiveAttributesCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public SetActiveAttributesCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(SetActiveAttributesCommand request, CancellationToken cancellationToken)
    {
        var idsFromDatabase = await appDbContext
            .AttributeValues
            .Where(x => x.UserId == request.UserId)
            .Select(x=>x.AttributeId)
            .ToListAsync(cancellationToken);

        var idsToRemove = idsFromDatabase.Except(request.AttributeIds);

        var toRemove = await appDbContext.AttributeValues.Where(x => idsToRemove.Contains(x.AttributeId)).ToListAsync(cancellationToken);
        appDbContext.AttributeValues.RemoveRange(toRemove);

        var idsToAdd = request.AttributeIds.Except(idsFromDatabase);

        foreach (var id in idsToAdd )
        {
            appDbContext.AttributeValues.Add(new() { AttributeId = id, UserId = request.UserId });
        }

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
