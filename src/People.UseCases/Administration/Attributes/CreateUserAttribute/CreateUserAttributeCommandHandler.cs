using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;

namespace People.UseCases.Administration.Attributes.CreateUserAttribute;

/// <inheritdoc cref="CreateUserAttributeCommand"/>
internal class CreateUserAttributeCommandHandler : AsyncRequestHandler<CreateUserAttributeCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateUserAttributeCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    protected override async Task Handle(CreateUserAttributeCommand request, CancellationToken cancellationToken)
    {
        var viewRoles = await appDbContext.Roles.Where(role => request.Attribute.ViewRolesIds.Contains(role.Id)).ToListAsync(cancellationToken);
        var editRoles = await appDbContext.Roles.Where(role => request.Attribute.EditRolesIds.Contains(role.Id)).ToListAsync(cancellationToken);

        var attribute = mapper.Map<UserAttribute>(request.Attribute);

        attribute.AllowEditRoles = editRoles;
        attribute.AllowViewRoles = viewRoles;

        if (attribute.AttributeType == AttributeType.DropDown)
        {
            attribute.AttributeOptions = mapper.Map<List<AttributeOption>>(request.Attribute.AttributeOptions.Where(attr=>!string.IsNullOrWhiteSpace(attr.Title)));

            foreach (var option in attribute.AttributeOptions)
            {
                option.Title = option.Title.Trim();
            }
        }

        await appDbContext.Attributes.AddAsync(attribute, cancellationToken);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
