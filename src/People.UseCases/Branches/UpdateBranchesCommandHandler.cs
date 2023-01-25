using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.Common.Utils;

namespace People.UseCases.Branches;

/// <inheritdoc cref="UpdateBranchDirectorCommand"/>
internal class UpdateBranchesCommandHandler : AsyncRequestHandler<UpdateBranchesCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly ICrmAccessor crmAccessor;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdateBranchesCommandHandler(IAppDbContext appDbContext, ICrmAccessor crmAccessor, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.crmAccessor = crmAccessor;
        this.mapper = mapper;
    }

    /// <inheritdoc/>.
    protected override async Task Handle(UpdateBranchesCommand request, CancellationToken cancellationToken)
    {
        var branches = await crmAccessor.GetBranchesAsync(cancellationToken);

        var mappedBranches = branches.Branches.Select(x => mapper.Map<Branch>(x));
        var storedBranches = await appDbContext.Branches.ToListAsync(cancellationToken);
        var differenceBetweenCollections = CollectionUtils.Diff(storedBranches, mappedBranches, (x, y) => x.CrmId == y.CrmId, (x, y) => x.Name == y.Name);

        if (!differenceBetweenCollections.HasDifferences)
        {
            return;
        }

        await appDbContext.Branches.AddRangeAsync(differenceBetweenCollections.Added, cancellationToken);

        foreach (var updatedBranch in differenceBetweenCollections.Updated)
        {
            updatedBranch.Target.Id = updatedBranch.Source.Id;
            updatedBranch.Target.DirectorId = updatedBranch.Source.DirectorId;
            appDbContext.Branches.Update(updatedBranch.Target);
        }

        appDbContext.Branches.RemoveRange(differenceBetweenCollections.Removed);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
