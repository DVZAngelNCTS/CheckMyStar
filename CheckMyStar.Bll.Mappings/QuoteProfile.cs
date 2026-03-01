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
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.IsAccepted, opt => opt.MapFrom(src => src.IsAccepted))
                .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.Reference)).ReverseMap();
        }
    }
}
