using AutoMapper;
using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleModel>()
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name)).ReverseMap();
        }
    }
}
