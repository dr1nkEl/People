using MediatR;
using People.Domain.Reviews.Entities;

namespace People.UseCases.PR.GetReply;

/// <summary>
/// Get reply of specific user in pr.
/// </summary>
/// <param name="PrId"></param>
/// <param name="UserId"></param>
public record GetReplyQuery(int PrId, int UserId) : IRequest<Reply>;
