using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.PR;
using People.UseCases.PR.GetTemplates;

namespace People.UseCases.PR.GetTemplatesForUser;

/// <summary>
/// Handler for <see cref="GetPrTemplatesForUserQuery"/>
/// </summary>
internal class GetPrTemplatesForUserQueryHandler : IRequestHandler<GetPrTemplatesForUserQuery, IEnumerable<PRTemplateDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    /// <param name="mapper"><see cref="IMapper"/>.</param>
    public GetPrTemplatesForUserQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PRTemplateDto>> Handle(GetPrTemplatesForUserQuery request, CancellationToken cancellationToken)
    {
        var user = await appDbContext.Users.Include(x => x.Positions).FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        var templates = await appDbContext.ReviewTemplates.ToListAsync(cancellationToken);

        var relatedTemplates = templates.Where(x => x.RelatedPositionId == null || user.Positions.Select(y => y.Id).Contains(x.Id)).ToList();

        return mapper.Map<IEnumerable<PRTemplateDto>>(relatedTemplates);
    }
}
