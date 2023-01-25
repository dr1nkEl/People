using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Attribute;

namespace People.UseCases.Administration.Attributes.GetUserAttributes;

/// <inheritdoc cref="GetUserAttributesQuery"/>
internal class GetUserAttributesQueryHandler : IRequestHandler<GetUserAttributesQuery, IEnumerable<UserAttributeDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetUserAttributesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<UserAttributeDto>> Handle(GetUserAttributesQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Attributes.AsQueryable();

        return await mapper.ProjectTo<UserAttributeDto>(query).ToListAsync(cancellationToken);
    }
}
