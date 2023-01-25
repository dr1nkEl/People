using MediatR;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Users;

/// <inheritdoc cref="DeleteUserCommand"/>
internal class DeleteUserCommandHandler : AsyncRequestHandler<DeleteUserCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeleteUserCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        foreach (var id in request.UserIds)
        {
            var user = await appDbContext.Users.GetAsync(x => x.Id == id, cancellationToken);
            user.DeletedAt = DateTime.UtcNow;
        }

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
