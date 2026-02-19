using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class AssessmentProfile : Profile
    {
        public AssessmentProfile()
        {
            CreateMap<Assessment, AssessmentModel>()
                .ForMember(dest => dest.Criteria, opts => opts.MapFrom(src => src.AssessmentCriteria));

            CreateMap<AssessmentModel, Assessment>()
                .ForMember(dest => dest.AssessmentCriteria, opts => opts.Ignore());

            CreateMap<AssessmentCriterion, AssessmentCriterionModel>();

            CreateMap<AssessmentCriterionModel, AssessmentCriterion>();
        }
    }
}
