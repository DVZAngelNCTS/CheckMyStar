using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class CivilityProfile : Profile
    {
        public CivilityProfile()
        {
            CreateMap<Civility, CivilityModel>()
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name)).ReverseMap();
        }
    }
}
