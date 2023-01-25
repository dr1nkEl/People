using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Attribute;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Administration.Attributes.GetAttributeOptions;

/// Handler for <inheritdoc cref="GetAttributeOptionsQuery"/>
internal class GetAttributeOptionsQueryHandler : IRequestHandler<GetAttributeOptionsQuery, IEnumerable<AttributeOptionDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetAttributeOptionsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<AttributeOptionDto>> Handle(GetAttributeOptionsQuery request, CancellationToken cancellationToken)
    {
        var query = await appDbContext.Attributes
            .Include(attr=>attr.AttributeOptions)
            .GetAsync(attr => attr.Id == request.AttributeId, cancellationToken);
        return mapper.Map<IEnumerable<AttributeOptionDto>>(query.AttributeOptions);
    }
}
