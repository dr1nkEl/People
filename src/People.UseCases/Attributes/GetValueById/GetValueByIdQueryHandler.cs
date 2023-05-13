using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.Attributes.GetValueById;

/// <summary>
/// Handler for <see cref="GetValueByIdQuery"/>.
/// </summary>
internal class GetValueByIdQueryHandler : IRequestHandler<GetValueByIdQuery, AttributeValue>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public GetValueByIdQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<AttributeValue> Handle(GetValueByIdQuery request, CancellationToken cancellationToken)
        => await appDbContext
        .AttributeValues
        .Include(x=>x.User)
        .Include(x=>x.Attribute)
        .ThenInclude(x=>x.AttributeOptions)
        .FirstAsync(x => x.Id == request.Id, cancellationToken);
}
