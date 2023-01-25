using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.GetTypes;

/// Handler for <inheritdoc cref="GetReviewTypesQuery"/>
internal class GetReviewTypesQueryHandler : IRequestHandler<GetReviewTypesQuery, IEnumerable<ReviewTypeDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">App DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public GetReviewTypesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ReviewTypeDto>> Handle(GetReviewTypesQuery request, CancellationToken cancellationToken)
    {
        return await mapper.ProjectTo<ReviewTypeDto>(appDbContext.ReviewTypes).ToListAsync(cancellationToken);
    }
}
