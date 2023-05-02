using MediatR;

namespace People.UseCases.PR.SetReview;

/// <summary>
/// Set review command.
/// </summary>
public record SetReviewCommand(int UserId, int TemplateId, DateTime? Deadline, IEnumerable<int> ReviewedByUserIds) : IRequest;
