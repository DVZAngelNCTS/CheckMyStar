using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class AccommodationProfile : Profile
    {
        public AccommodationProfile()
        {
            CreateMap<Accommodation, AccommodationModel>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.AccommodationName, opt => opt.MapFrom(src => src.AccommodationName))
                .ForMember(dest => dest.AccommodationPhone, opt => opt.MapFrom(src => src.AccommodationPhone))
                .ForMember(dest => dest.AccommodationCurrentStar, opt => opt.MapFrom(src => src.AccommodationCurrentStar))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.Address, opt => opt.Ignore())
                .ForMember(dest => dest.AccommodationType, opt => opt.Ignore());

            CreateMap<AccommodationModel, Accommodation>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.AccommodationName, opt => opt.MapFrom(src => src.AccommodationName))
                .ForMember(dest => dest.AccommodationPhone, opt => opt.MapFrom(src => src.AccommodationPhone))
                .ForMember(dest => dest.AccommodationTypeIdentifier, opt => opt.MapFrom(src => src.AccommodationType.Identifier))
                .ForMember(dest => dest.AccommodationCurrentStar, opt => opt.MapFrom(src => src.AccommodationCurrentStar))
                .ForMember(dest => dest.AddressIdentifier, opt => opt.MapFrom(src => src.Address.Identifier))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));

            CreateMap<AccommodationType, AccommodationTypeModel>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));

            CreateMap<AccommodationTypeModel, AccommodationType>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));

            CreateMap<AccommodationResult, AccommodationResponse>()
                .ForMember(dest => dest.Accommodation, opt => opt.MapFrom(src => src.Accommodation ?? null))
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.IsSuccess))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
        }
    }
}
