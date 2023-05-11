using AutoMapper;
using People.Domain.Users.Entities;
using People.UseCases.Administration.Attributes.CreateUserAttribute;
using People.UseCases.Common.Dtos.Administration;
using People.UseCases.Common.Dtos.Attribute;

namespace People.UseCases.Administration;

/// <summary>
/// Admin mapping profile.
/// </summary>
public class AdminMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AdminMappingProfile()
    {
        CreateMap<AppIdentityRole, RoleDto>().ReverseMap();
        CreateMap<UserAttribute, UserAttributeDto>().ReverseMap();
        CreateMap<UserAttribute, DetailedUserAttributeDto>();
        CreateMap<AttributeOption, UserAttributeDto>();
        CreateMap<AttributeOption, AttributeOptionDto>();
        CreateMap<NewAttributeDto, UserAttribute>();
    }
}
