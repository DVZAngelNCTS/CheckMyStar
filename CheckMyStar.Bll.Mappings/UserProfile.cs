using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => src.Password))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Society, opts => opts.MapFrom(src => src.Society)).ReverseMap();
        }
    }
}
