using AutoMapper;
using People.Domain.Reviews.Entities;
using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.PR;

namespace People.UseCases.PR;

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
        CreateMap<ReviewTemplate, PRTemplateDto>();
        CreateMap<ReviewTemplate, PRTemplateDetailedDto>().ReverseMap();
        CreateMap<Question, QuestionDto>().ReverseMap();
        CreateMap<QuestionOption, QuestionOptionDto>().ReverseMap();
        CreateMap<ReviewType, ReviewTypeDto>();
        CreateMap<NewTemplateDto, ReviewTemplate>();
        CreateMap<NewReviewTypeDto, ReviewType>();
    }
}
