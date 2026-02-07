using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Results;
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
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive)).ReverseMap();

            CreateMap<RoleResult, RoleResponse>()
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role ?? null));

            CreateMap<RolesResult, RolesResponse>()
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Roles ?? null));
        }
    }
}
