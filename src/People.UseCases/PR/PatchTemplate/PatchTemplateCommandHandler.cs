using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.EFCore;

namespace People.UseCases.PR.PatchTemplate;

/// Handler for <inheritdoc cref="PatchTemplateCommand"/>
internal class PatchTemplateCommandHandler : AsyncRequestHandler<PatchTemplateCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">App DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public PatchTemplateCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(PatchTemplateCommand request, CancellationToken cancellationToken)
    {
        var entity = await appDbContext.ReviewTemplates
            .Include(template=>template.FeedbackQuestions)
            .Include(template=>template.ReviewedUserQuestions)
            .GetAsync(template => template.Id == request.Template.Id, cancellationToken);
        var mapped = mapper.Map<ReviewTemplate>(request.Template);
        entity.Name = mapped.Name;
        entity.FeedbackQuestions = mapped.FeedbackQuestions;
        entity.ReviewedUserQuestions = mapped.ReviewedUserQuestions;
        entity.RelatedPositionId = mapped.RelatedPositionId;
        entity.ReviewTypeId = mapped.ReviewTypeId;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
