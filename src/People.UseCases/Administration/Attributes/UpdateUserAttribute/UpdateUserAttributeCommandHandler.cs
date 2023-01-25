using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.UseCases.Common.Dtos.Attribute;
using Saritasa.Tools.EFCore;

namespace People.UseCases.Administration.Attributes.UpdateUserAttribute;

/// <inheritdoc cref="UpdateUserAttributeCommand"/>
internal class UpdateUserAttributeCommandHandler : AsyncRequestHandler<UpdateUserAttributeCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpdateUserAttributeCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(UpdateUserAttributeCommand request, CancellationToken cancellationToken)
    {
        await Update(request.EditedAttribute, cancellationToken);
    }

    private async Task Update(EditAttributeDto updatedAttribute, CancellationToken cancellationToken)
    {
        var viewRoles = await appDbContext.Roles.Where(role => updatedAttribute.AllowViewRoles.Contains(role.Id)).ToListAsync(cancellationToken);
        var editRoles = await appDbContext.Roles.Where(role => updatedAttribute.AllowEditRoles.Contains(role.Id)).ToListAsync(cancellationToken);

        var attr = await appDbContext.Attributes
            .AsQueryable()
            .Include(x=>x.AllowEditRoles)
            .Include(x=>x.AllowViewRoles)
            .Include(x=>x.AttributeOptions)
            .GetAsync(attr => attr.Id == updatedAttribute.Id, cancellationToken);

        attr.AllowEditRoles = editRoles;
        attr.AllowViewRoles = viewRoles;
        attr.Name = updatedAttribute.Name;
        attr.AllowViewSelf = updatedAttribute.AllowViewSelf;
        attr.AllowEditSelf = updatedAttribute.AllowEditSelf;
        attr.AttributeType = updatedAttribute.AttributeType;

        if (attr.AttributeType == AttributeType.DropDown)
        {
            attr.AttributeOptions = mapper.Map<ICollection<AttributeOption>>(updatedAttribute.AttributeOptions.Where(attr=>!string.IsNullOrWhiteSpace(attr.Title)));

            foreach (var option in attr.AttributeOptions)
            {
                option.Title = option.Title.Trim();
            }
        }
        else
        {
            attr.AttributeOptions = null;
        }

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
