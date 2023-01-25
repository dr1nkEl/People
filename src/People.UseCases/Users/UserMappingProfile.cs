using AutoMapper;
using People.Domain.Users.Entities;
using People.DomainServices;
using People.UseCases.Common.Dtos.Attribute;
using People.UseCases.Common.Dtos.Position;
using People.UseCases.Common.Dtos.User;

namespace People.UseCases.Users;

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
        CreateMap<User, DetailedUserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<Position, PositionDto>();
        CreateMap<Infrastructure.Common.Crm.Dto.UserDto, User>()
            .ForMember(entity => entity.Birthday, options => options.MapFrom(dto => DateOnly.FromDateTime(dto.BirthDay.GetValueOrDefault())))
            .ForMember(entity => entity.Birthday, options => options.MapFrom(dto => DateOnly.FromDateTime(dto.BirthDay.GetValueOrDefault())))
            .ForMember(entity => entity.UserName, options => options.MapFrom(dto=>dto.Email))
            .ForMember(entity => entity.CrmId, options => options.MapFrom(dto=>dto.Id))
            .ForMember(entity => entity.Id, options => options.Ignore());
        CreateMap<User, UserInfoDto>()
            .ForMember(viewModel => viewModel.AvatarUrl, options => options.MapFrom(user => UserService.CreateGravatarUrl(user.Email)));
        CreateMap<AttributeOptionDto, AttributeOption>();
        CreateMap<User, UserShortInfoDto>()
            .ForMember(dto => dto.AvatarUrl, options => options.MapFrom(user => UserService.CreateGravatarUrl(user.Email)));
    }
}
