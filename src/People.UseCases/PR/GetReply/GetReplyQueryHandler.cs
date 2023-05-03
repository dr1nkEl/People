using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.PR.GetReply;

/// <summary>
/// Handler for <see cref="GetReplyQuery"/>.
/// </summary>
internal class GetReplyQueryHandler : IRequestHandler<GetReplyQuery, Reply>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public GetReplyQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<Reply> Handle(GetReplyQuery request, CancellationToken cancellationToken)
    {
        var pr = await appDbContext.PerformanceReviews
            .Include(x=>x.ReviewedUserReply)
            .ThenInclude(x=>x.Answers)
            .Include(x=>x.Feedback)
            .ThenInclude(x=>x.Answers)
            .ThenInclude(x=>x.Question)
            .FirstAsync(x => x.Id == request.PrId);

        if (request.UserId == pr.ReviewedUserId)
        {
            return pr.ReviewedUserReply;
        }

        return pr.Feedback.FirstOrDefault(x=>x.UserId == request.UserId);
    }
}
