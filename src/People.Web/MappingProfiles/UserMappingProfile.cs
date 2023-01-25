using AutoMapper;
using People.UseCases.Common.Dtos.Attribute;
using People.DomainServices;
using People.UseCases.Common.Dtos.User;
using People.UseCases.Users;
using People.Web.ViewModels;
using People.Web.ViewModels.Attribute;
using People.Web.ViewModels.Position;
using People.Web.ViewModels.User;
using People.UseCases.Common.Dtos.Position;

namespace People.Web.MappingProfiles;

/// <summary>
/// User mapping profile.
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<UserDto, UserViewModel>().ReverseMap();
        CreateMap<UserAttributeDto, AttributeViewModel>();
        CreateMap<AttributeOptionDto, AttributeOptionViewModel>();
        CreateMap<DetailedUserAttributeDto, DetailedAttributeViewModel>();
        CreateMap<NewAttributeViewModel, NewAttributeDto>()
            .ForMember(entity=>entity.AttributeOptions,
                options=>options.MapFrom(dto=>dto.AttributeTitles.Select(title=>new AttributeOptionDto() { Title=title })));
        CreateMap<EditAttributeViewModel, EditAttributeDto>()
            .ForMember(entity => entity.Id, options => options.MapFrom(vm => vm.Attribute.Id))
            .ForMember(entity => entity.Name, options => options.MapFrom(vm => vm.Attribute.Name))
            .ForMember(entity => entity.AllowEditSelf, options => options.MapFrom(vm => vm.Attribute.AllowEditSelf))
            .ForMember(entity => entity.AllowViewSelf, options => options.MapFrom(vm => vm.Attribute.AllowViewSelf))
            .ForMember(entity => entity.AttributeOptions,
                options => options.MapFrom(dto => dto.AttributeTitles.Select(title => new AttributeOptionDto() { Title = title })))
            .ForMember(entity=>entity.AttributeType, options=>options.MapFrom(dto=>dto.Attribute.AttributeType));
        CreateMap<UserDto, UserViewModel>()
            .ForMember(entity=>entity.Avatar, options => options.MapFrom(dto => UserService.CreateGravatarUrl(dto.Email)));
        CreateMap<SearchBranchUsersViewModel, GetAllUsersDetailedQuery>();
        CreateMap<DetailedUserDto, UserDetailedViewModel>()
            .ForMember(entity=>entity.Avatar, options=>options.MapFrom(dto => UserService.CreateGravatarUrl(dto.Email)));
        CreateMap<PositionDto, PositionViewModel>();
    }
}
