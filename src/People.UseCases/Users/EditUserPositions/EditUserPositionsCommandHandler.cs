using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Users.AddUserToPosition;

/// <summary>
/// Edit user positions.
/// </summary>
internal class EditUserPositionsCommandHandler : AsyncRequestHandler<EditUserPositionsCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public EditUserPositionsCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc />
    protected override async Task Handle(EditUserPositionsCommand request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Users.Include(usr => usr.Positions);
        var user = await query.GetAsync(usr => usr.Id == request.UserId, cancellationToken);
        var positions = await appDbContext.Positions.Where(pos => request.PositionIds
            .Any(positionId => positionId == pos.Id))
            .ToListAsync(cancellationToken);
        user.Positions.Clear();
        foreach (var position in positions)
        {
            user.Positions.Add(position);
        }

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
