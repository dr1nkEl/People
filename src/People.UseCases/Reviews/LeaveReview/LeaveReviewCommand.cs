using MediatR;
using People.Domain.Reviews.Entities;

namespace People.UseCases.Reviews.LeaveReview;

/// <summary>
/// Leave review command.
/// </summary>
/// <param name="UserReply"><see cref="Reply"/>.</param>
/// <param name="PrId">Id of related PR.</param>
public record LeaveReviewCommand(Reply UserReply, int PrId) : IRequest;
