using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class AssessmentResultProfile : Profile
    {
        public AssessmentResultProfile()
        {
            CreateMap<AssessmentResult, AssessmentResultModel>();

            CreateMap<AssessmentResultModel, AssessmentResult>();
        }
    }
}
