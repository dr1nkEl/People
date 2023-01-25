using AutoMapper;
using MediatR;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.PR.CreateType;

/// Handler for <inheritdoc cref="CreateReviewTypeCommand"/>
internal class CreateReviewTypeCommandHandler : AsyncRequestHandler<CreateReviewTypeCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">App DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public CreateReviewTypeCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(CreateReviewTypeCommand request, CancellationToken cancellationToken)
    {
        var item = mapper.Map<ReviewType>(request.ReviewTypeDto);
        if (item.Interval is null)
        {
            item.IntervalAmount = null;
        }
        await appDbContext.ReviewTypes.AddAsync(item, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
