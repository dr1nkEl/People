using AutoMapper;
using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.Branch;

namespace People.UseCases.Branches;

/// <summary>
/// Branch mapping profile.
/// </summary>
internal class BranchMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BranchMappingProfile()
    {
        CreateMap<Branch, BranchDto>().ReverseMap();
        CreateMap<Branch, BranchNameDto>();
    }
}
