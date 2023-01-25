using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Users.AddReportingUsers;

/// <summary>
/// Edit reporting users.
/// </summary>
internal class EditReportingUsersCommandHandler : AsyncRequestHandler<EditReportingUsersCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public EditReportingUsersCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc />
    protected override async Task Handle(EditReportingUsersCommand request, CancellationToken cancellationToken)
    {
        var user = await appDbContext.Users.GetAsync(usr => usr.Id == request.UserId, cancellationToken);
        var reportingUsers = await appDbContext.Users.Where(user => request.ReportingUserIds
            .Any(userId => userId == user.Id))
            .ToListAsync(cancellationToken);
        var userHierarchies = new List<UserPositionHierarchy>();
        foreach (var reportingUser in reportingUsers)
        {
            var userHierarchy = new UserPositionHierarchy()
            {
                ParentUser = user,
                ParentUserId = user.Id,
                ChildUser = reportingUser,
                ChildUserId = reportingUser.Id
            };
            userHierarchies.Add(userHierarchy);
        }

        var oldUserHierarchies = await appDbContext.UserPositionHierarchies.Where(hierarchy => hierarchy.ParentUserId == request.UserId)
            .ToListAsync(cancellationToken);
        appDbContext.UserPositionHierarchies.RemoveRange(oldUserHierarchies);
        await appDbContext.UserPositionHierarchies.AddRangeAsync(userHierarchies, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
