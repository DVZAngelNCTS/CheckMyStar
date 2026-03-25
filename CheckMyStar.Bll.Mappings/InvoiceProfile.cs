using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceModel>()
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.QuoteIdentifier, opt => opt.MapFrom(src => src.QuoteIdentifier))
                .ForMember(dest => dest.ClientUserIdentifier, opt => opt.MapFrom(src => src.ClientUserIdentifier))
                .ForMember(dest => dest.ClientAddressIdentifier, opt => opt.MapFrom(src => src.ClientAddressIdentifier))
                .ForMember(dest => dest.CompanySocietyIdentifier, opt => opt.MapFrom(src => src.CompanySocietyIdentifier))
                .ForMember(dest => dest.CompanyAddressIdentifier, opt => opt.MapFrom(src => src.CompanyAddressIdentifier))
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.InvoiceDate))
                .ForMember(dest => dest.TotalAmountHT, opt => opt.MapFrom(src => src.TotalAmountHT))
                .ForMember(dest => dest.TotalVATAmount, opt => opt.MapFrom(src => src.TotalVATAmount))
                .ForMember(dest => dest.TotalAmountTTC, opt => opt.MapFrom(src => src.TotalAmountTTC))
                .ForMember(dest => dest.PaymentStatusIdentifier, opt => opt.MapFrom(src => src.PaymentStatusIdentifier))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.ClientUser, opt => opt.Ignore())
                .ForMember(dest => dest.ClientAddress, opt => opt.Ignore())
                .ForMember(dest => dest.CompanySociety, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyAddress, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceLines, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.InvoiceLines, opt => opt.Ignore());

            CreateMap<InvoiceLine, InvoiceLineModel>().ReverseMap();
        }
    }
}
