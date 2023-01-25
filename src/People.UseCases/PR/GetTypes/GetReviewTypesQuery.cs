using MediatR;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.GetTypes;

/// <summary>
/// Get review types query.
/// </summary>
public record GetReviewTypesQuery : IRequest<IEnumerable<ReviewTypeDto>>;
