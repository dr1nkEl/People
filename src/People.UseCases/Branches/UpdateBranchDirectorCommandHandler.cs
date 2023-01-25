using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Branches;

/// <inheritdoc cref="UpdateBranchDirectorCommand"/>
internal class UpdateBranchDirectorCommandHandler : AsyncRequestHandler<UpdateBranchDirectorCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdateBranchDirectorCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>.
    protected override async Task Handle(UpdateBranchDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = await appDbContext.Users.Where(x => x.DeletedAt == null).FirstOrDefaultAsync(x => x.Id == request.DirectorId, cancellationToken);

        var branch = await appDbContext.Branches.GetAsync(x => x.Id == request.BranchId, cancellationToken);

        branch.DirectorId = request.DirectorId;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
