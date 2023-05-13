using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.Users.SetValueOfAttribute;

/// <summary>
/// Handler for <see cref="SetValueOfAttributeCommand"/>.
/// </summary>
internal class SetValueOfAttributeCommandHandler : AsyncRequestHandler<SetValueOfAttributeCommand>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext"><see cref="IAppDbContext"/>.</param>
    public SetValueOfAttributeCommandHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(SetValueOfAttributeCommand request, CancellationToken cancellationToken)
    {
        var value = await appDbContext.AttributeValues.FirstAsync(x => x.Id == request.ValueId);

        value.Value = request.Value;

        appDbContext.AttributeValues.Update(value);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
