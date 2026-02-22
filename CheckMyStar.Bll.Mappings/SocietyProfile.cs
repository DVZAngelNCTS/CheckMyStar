using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings;

public class SocietyProfile : Profile
{
    public SocietyProfile()
    {
        CreateMap<SocietyModel, Society>()
            .ForMember(dest => dest.AddressIdentifier, opts => opts.MapFrom(src => src.AddressIdentifier))
            .ForMember(dest => dest.CreatedDate, opts => opts.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
            .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
            .ForMember(dest => dest.UpdatedDate, opts => opts.MapFrom(src => src.UpdatedDate)).ReverseMap();            
    }
}