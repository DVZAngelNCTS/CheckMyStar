using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressModel>()
                .ForMember(dest => dest.AddressLine, opt => opt.MapFrom(src => src.AddressLine))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));

            CreateMap<AddressModel, Address>()
                .ForMember(dest => dest.AddressLine, opt => opt.MapFrom(src => src.AddressLine))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));

            CreateMap<AddressResult, AddressResponse>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address ?? null));
        }
    }
}