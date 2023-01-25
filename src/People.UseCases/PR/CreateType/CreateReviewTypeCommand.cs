using MediatR;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.CreateType;

/// <summary>
/// Create review type command.
/// </summary>
/// <param name="ReviewTypeDto">Review type DTO.</param>
public record CreateReviewTypeCommand(NewReviewTypeDto ReviewTypeDto) : IRequest;
