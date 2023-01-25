using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR.GetTemplates;

/// Handler for <inheritdoc cref="GetPRTemplatesQuery"/>
internal class GetPRTemplatesQueryHandler : IRequestHandler<GetPRTemplatesQuery, IEnumerable<PRTemplateDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">App DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public GetPRTemplatesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PRTemplateDto>> Handle(GetPRTemplatesQuery request, CancellationToken cancellationToken)
    {
        return await mapper.ProjectTo<PRTemplateDto>(appDbContext.ReviewTemplates).ToListAsync(cancellationToken);
    }
}
