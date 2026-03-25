using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class QuoteProfile : Profile
    {
        public QuoteProfile() 
        {
            CreateMap<Quote, QuoteModel>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.ClientUserIdentifier, opt => opt.MapFrom(src => src.ClientUserIdentifier))
                .ForMember(dest => dest.ClientAddressIdentifier, opt => opt.MapFrom(src => src.ClientAddressIdentifier))
                .ForMember(dest => dest.InspectorIdentifier, opt => opt.MapFrom(src => src.InspectorIdentifier))
                .ForMember(dest => dest.CompanySocietyIdentifier, opt => opt.MapFrom(src => src.CompanySocietyIdentifier))
                .ForMember(dest => dest.CompanyAddressIdentifier, opt => opt.MapFrom(src => src.CompanyAddressIdentifier))
                .ForMember(dest => dest.CompanyLogoPath, opt => opt.MapFrom(src => src.CompanyLogoPath))
                .ForMember(dest => dest.CompanyEmail, opt => opt.MapFrom(src => src.CompanyEmail))
                .ForMember(dest => dest.CompanyPhone, opt => opt.MapFrom(src => src.CompanyPhone))
                .ForMember(dest => dest.CompanySiretCode, opt => opt.MapFrom(src => src.CompanySiretCode))
                .ForMember(dest => dest.CompanyVatNumber, opt => opt.MapFrom(src => src.CompanyVatNumber))
                .ForMember(dest => dest.CompanyLegalInformation, opt => opt.MapFrom(src => src.CompanyLegalInformation))
                .ForMember(dest => dest.TotalAmountHT, opt => opt.MapFrom(src => src.TotalAmountHT))
                .ForMember(dest => dest.TotalAmountTTC, opt => opt.MapFrom(src => src.TotalAmountTTC))
                .ForMember(dest => dest.ValidityDate, opt => opt.MapFrom(src => src.ValidityDate))
                .ForMember(dest => dest.ExecutionDate, opt => opt.MapFrom(src => src.ExecutionDate))
                .ForMember(dest => dest.QuoteStatusIdentifier, opt => opt.MapFrom(src => src.QuoteStatusIdentifier))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.IsEditable, opt => opt.MapFrom(src => src.IsEditable))
                .ForMember(dest => dest.ClientUser, opt => opt.Ignore())
                .ForMember(dest => dest.ClientAddress, opt => opt.Ignore())
                .ForMember(dest => dest.Inspector, opt => opt.Ignore())
                .ForMember(dest => dest.CompanySociety, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyAddress, opt => opt.Ignore())
                .ForMember(dest => dest.QuoteLines, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.QuoteLines, opt => opt.Ignore());

            CreateMap<QuoteLine, QuoteLineModel>().ReverseMap();
        }
    }
}

