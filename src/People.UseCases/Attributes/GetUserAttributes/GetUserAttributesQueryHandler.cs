using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.Attributes.GetUserAttributes;

/// <summary>
/// Handler for <see cref="GetUserAttributesByIdQuery"/>.
/// </summary>
internal class GetUserAttributesQueryHandler : IRequestHandler<GetUserAttributesByIdQuery, IEnumerable<UserAttribute>>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public GetUserAttributesQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<UserAttribute>> Handle(GetUserAttributesByIdQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext
            .Attributes
            .Include(x => x.Values)
            .Include(x => x.AllowViewRoles)
            .Include(x => x.AllowEditRoles)
            .Include(x=>x.AttributeOptions)
            .Where(x => x.Values.Any(x => x.UserId == request.UserId))
            .ToListAsync(cancellationToken);
    }
}
