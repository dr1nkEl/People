using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.PR;
using Saritasa.Tools.EFCore;

namespace People.UseCases.PR.GetTemplate;

/// Handler for <inheritdoc cref="GetPRTemplateQuery"/>
internal class GetPRTemplateQueryHandler : IRequestHandler<GetPRTemplateQuery, PRTemplateDetailedDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">App DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public GetPRTemplateQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<PRTemplateDetailedDto> Handle(GetPRTemplateQuery request, CancellationToken cancellationToken)
    {
        var item = await appDbContext.ReviewTemplates
            .Include(template => template.FeedbackQuestions)
            .Include(template => template.ReviewedUserQuestions)
            .GetAsync(template => template.Id == request.Id, cancellationToken);
        return mapper.Map<PRTemplateDetailedDto>(item);
    }
}
