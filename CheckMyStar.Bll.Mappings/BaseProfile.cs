using AutoMapper;

using CheckMyStar.Dal.Results;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Mappings
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<BaseResult, BaseResponse>()
                .ForMember(dest => dest.IsSuccess, opts => opts.MapFrom(src => src.IsSuccess))
                .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.Message));
        }
    }
}
