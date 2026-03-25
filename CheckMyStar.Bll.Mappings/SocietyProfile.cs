using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings;

public class SocietyProfile : Profile
{
    public SocietyProfile()
    {
        CreateMap<SocietyModel, Society>()
            .ForMember(dest => dest.CreatedDate, opts => opts.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
            .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.LegalInformation, opts => opts.MapFrom(src => src.LegalInformation))
            .ForMember(dest => dest.LogoPath, opts => opts.MapFrom(src => src.LogoPath))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
            .ForMember(dest => dest.SiretCode, opts => opts.MapFrom(src => src.SiretCode))
            .ForMember(dest => dest.VatNumber, opts => opts.MapFrom(src => src.VatNumber))
            .ForMember(dest => dest.CreatedDate, opts => opts.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opts => opts.MapFrom(src => src.UpdatedDate)).ReverseMap();

        CreateMap<SocietyResult, SocietyResponse>();
    }
}