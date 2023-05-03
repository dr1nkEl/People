using MediatR;

namespace People.UseCases.PR.MarkTimeoutReviews;

/// <summary>
/// Finds and marks timeouted reviews.
/// </summary>
public record MarkTimeoutReviewsCommand : IRequest;
