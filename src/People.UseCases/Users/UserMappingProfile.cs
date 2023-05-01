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
        CreateMap<User, UserInfoDto>()
            .ForMember(viewModel => viewModel.AvatarUrl, options => options.MapFrom(user => UserService.CreateGravatarUrl(user.Email)));
        CreateMap<AttributeOptionDto, AttributeOption>();
        CreateMap<User, UserShortInfoDto>()
            .ForMember(dto => dto.AvatarUrl, options => options.MapFrom(user => UserService.CreateGravatarUrl(user.Email)));
    }
}
