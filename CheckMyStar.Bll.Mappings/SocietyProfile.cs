using AutoMapper;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Bll.Mappings;

public class SocietyProfile : Profile
{
    public SocietyProfile()
    {
        CreateMap<SocietyCreateRequest, Society>()
            .ForMember(dest => dest.Identifier, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());
    }
}