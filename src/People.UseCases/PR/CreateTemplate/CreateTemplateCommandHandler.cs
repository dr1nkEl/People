using AutoMapper;
using MediatR;
using People.Domain.Reviews.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.PR.CreateTemplate;

/// Handler for <inheritdoc cref="CreateTemplateCommand"/>
internal class CreateTemplateCommandHandler : AsyncRequestHandler<CreateTemplateCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">App DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public CreateTemplateCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
        var item = mapper.Map<ReviewTemplate>(request.Template);
        await appDbContext.ReviewTemplates.AddAsync(item, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
