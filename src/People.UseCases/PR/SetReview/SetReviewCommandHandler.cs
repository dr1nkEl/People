using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.Domain.Exceptions;

namespace People.UseCases.PR.SetReview;

/// <summary>
/// Handler for <see cref="SetReviewCommand"/>.
/// </summary>
internal class SetReviewCommandHandler : AsyncRequestHandler<SetReviewCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    /// <param name="loggedUserAccessor"><see cref="ILoggedUserAccessor"/>.</param>
    public SetReviewCommandHandler(IAppDbContext appDbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.appDbContext = appDbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc/>
    protected override async Task Handle(SetReviewCommand request, CancellationToken cancellationToken)
    {
        var template = await appDbContext
            .ReviewTemplates
            .Include(x=>x.FeedbackQuestions)
            .Include(x=>x.ReviewedUserQuestions)
            .Where(x => x.Id == request.TemplateId)
            .FirstOrDefaultAsync(cancellationToken);

        var feedBackUsers = await appDbContext.Users.Where(x => request.ReviewedByUserIds.Contains(x.Id) && x.Id != request.UserId).ToListAsync(cancellationToken);

        var review = new PerformanceReview()
        {
            CreatedAt = DateTime.UtcNow,
            ReviewedUserId = request.UserId,
            CreatedById = loggedUserAccessor.GetCurrentUserId(),
            ReviewedUserQuestions = template.ReviewedUserQuestions,
            FeedbackQuestions = template.FeedbackQuestions,
            FeedbackUsers = feedBackUsers,
        };

        if (request.Deadline != null)
        {
            review.Deadline = DateOnly.FromDateTime(request.Deadline.Value.ToUniversalTime());

            if (review.Deadline < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new DomainException("Неправильная дата.");
            }
        }

        appDbContext.PerformanceReviews.Add(review);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
