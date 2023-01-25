using MediatR;
using People.UseCases.Branches;
using People.UseCases.Users;

namespace People.Web.BackgroundJobs;

/// <summary>
/// Sync of crm branch data and DB branch data.
/// </summary>
public class SyncCrmDataJob
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    public SyncCrmDataJob(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Syncing data.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task SyncData(CancellationToken cancellationToken)
    {
        await UpdateBranches(cancellationToken);
        await UpdateUsers(cancellationToken);
    }

    private async Task UpdateBranches(CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateBranchesCommand(), cancellationToken);
    }

    private async Task UpdateUsers(CancellationToken cancellationToken)
    {
        await mediator.Send(new SyncUsersCommand(), cancellationToken);
    }
}
