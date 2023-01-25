using AutoMapper;
using People.UseCases.Users.AuthenticateUser;
using People.Web.ViewModels;

namespace People.Web.MappingProfiles;

/// <summary>
/// Mapper for mapping.
/// </summary>
public class LoginMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LoginMappingProfile()
    {
        CreateMap<LoginViewModel, LoginUserCommand>();
    }
}
