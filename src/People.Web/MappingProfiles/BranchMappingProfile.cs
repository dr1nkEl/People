using AutoMapper;
using People.UseCases.Common.Dtos.Branch;
using People.Web.ViewModels;

namespace People.Web.MappingProfiles;

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
        CreateMap<BranchDto, BranchViewModel>();
    }
}
