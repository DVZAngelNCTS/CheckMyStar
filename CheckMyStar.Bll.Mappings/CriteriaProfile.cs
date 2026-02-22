using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class CriteriaProfile : Profile
    {
        public CriteriaProfile()
        {
            CreateMap<StarLevelCriterionModel, StarLevelCriterion>()
                .ForMember(dest => dest.StarLevelId, opt => opt.MapFrom(src => src.StarLevelId))
                .ForMember(dest => dest.TypeCode, opt => opt.MapFrom(src => src.TypeCode))
                .ForMember(dest => dest.CriterionId, opt => opt.MapFrom(src => src.CriterionId));

            CreateMap<StarCriterionModel, Criterion>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.BasePoints, opt => opt.MapFrom(src => src.BasePoints))
                .ForMember(dest => dest.CriterionId, opt => opt.MapFrom(src => src.CriterionId));

            CreateMap<CriterionModel, Criterion>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.BasePoints, opt => opt.MapFrom(src => src.BasePoints))
                .ForMember(dest => dest.CriterionId, opt => opt.MapFrom(src => src.CriterionId));

            CreateMap<StarLevelModel, StarLevel>()
                .ForMember(dest => dest.StarLevelId, opt => opt.MapFrom(src => src.StarLevelId))
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate));
        }
    }
}
