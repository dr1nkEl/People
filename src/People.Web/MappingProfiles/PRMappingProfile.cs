using AutoMapper;
using Hangfire;
using People.UseCases.Common.Dtos.PR;
using People.Web.ViewModels.PR;
using Saritasa.Tools.Domain.Exceptions;

namespace People.Web.MappingProfiles;

/// <summary>
/// Perfomance review mapping profile.
/// </summary>
public class PRMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PRMappingProfile()
    {
        CreateMap<PRTemplateDto, TemplateViewModel>();
        CreateMap<PRTemplateDetailedDto, TemplateExtendedViewModel>();
        CreateMap<QuestionDto, QuestionViewModel>().ReverseMap();
        CreateMap<QuestionOptionDto, QuestionOptionViewModel>().ReverseMap();
        CreateMap<ReviewTypeDto, ReviewTypeViewModel>();
        CreateMap<TemplateExtendedViewModel, NewTemplateDto>();
        CreateMap<TemplateExtendedViewModel, PRTemplateDetailedDto>();
        CreateMap<NewReviewTypeViewModel, NewReviewTypeDto>()
            .ForMember(dto=>dto.Name, options=>options.MapFrom(vm=>vm.Name.Trim()));
    }
}
