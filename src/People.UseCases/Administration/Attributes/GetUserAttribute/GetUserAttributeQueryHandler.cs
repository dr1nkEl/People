using AutoMapper;
using MediatR;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Attribute;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Administration.Attributes.GetUserAttribute;

/// <inheritdoc cref="GetUserAttributeQuery"/>
internal class GetUserAttributeQueryHandler : IRequestHandler<GetUserAttributeQuery, DetailedUserAttributeDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetUserAttributeQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<DetailedUserAttributeDto> Handle(GetUserAttributeQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Attributes.AsQueryable();
        var item = await mapper.ProjectTo<DetailedUserAttributeDto>(query).GetAsync(attribute => attribute.Id == request.Id, cancellationToken);
        return item;
    }
}
