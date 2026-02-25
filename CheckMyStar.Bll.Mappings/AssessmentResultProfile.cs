using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class AssessmentResultProfile : Profile
    {
        public AssessmentResultProfile()
        {
            CreateMap<AssessmentResultEntity, AssessmentResultModel>();
            CreateMap<AssessmentResultModel, AssessmentResultEntity>();
        }
    }
}
