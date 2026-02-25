using AutoMapper;

using CheckMyStar.Bll.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Bll.Mappings
{
    public class AssessmentProfile : Profile
    {
        public AssessmentProfile()
        {
            CreateMap<Assessment, AssessmentModel>()
                .ForMember(dest => dest.Criteria, opts => opts.MapFrom(src => src.AssessmentCriteria));

            CreateMap<AssessmentModel, Assessment>()
                .ForMember(dest => dest.Identifier, opts => opts.MapFrom(src => src.Identifier))
                .ForMember(dest => dest.Capacity, opts => opts.MapFrom(src => src.Capacity))
                .ForMember(dest => dest.CreatedDate, opts => opts.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.FolderIdentifier, opts => opts.MapFrom(src => src.FolderIdentifier))
                .ForMember(dest => dest.IsComplete, opts => opts.MapFrom(src => src.IsComplete))
                .ForMember(dest => dest.IsDromTom, opts => opts.MapFrom(src => src.IsDromTom))
                .ForMember(dest => dest.IsHighMountain, opts => opts.MapFrom(src => src.IsHighMountain))
                .ForMember(dest => dest.IsBuildingClassified, opts => opts.MapFrom(src => src.IsBuildingClassified))
                .ForMember(dest => dest.IsStudioNoLivingRoom, opts => opts.MapFrom(src => src.IsStudioNoLivingRoom))
                .ForMember(dest => dest.IsParkingImpossible, opts => opts.MapFrom(src => src.IsParkingImpossible))
                .ForMember(dest => dest.IsWhiteZone, opts => opts.MapFrom(src => src.IsWhiteZone))
                .ForMember(dest => dest.NumberOfFloors, opts => opts.MapFrom(src => src.NumberOfFloors))
                .ForMember(dest => dest.NumberOfRooms, opts => opts.MapFrom(src => src.NumberOfRooms))
                .ForMember(dest => dest.SmallestRoomArea, opts => opts.MapFrom(src => src.SmallestRoomArea))
                .ForMember(dest => dest.TargetStarLevel, opts => opts.MapFrom(src => src.TargetStarLevel))
                .ForMember(dest => dest.TotalArea, opts => opts.MapFrom(src => src.TotalArea))
                .ForMember(dest => dest.TotalRoomsArea, opts => opts.MapFrom(src => src.TotalRoomsArea)).ReverseMap();

            CreateMap<AssessmentCriterion, AssessmentCriterionModel>();

            CreateMap<AssessmentCriterionModel, AssessmentCriterion>();

            CreateMap<AssessmentCriterionDetail, AssessmentCriterionDetailModel>();
        }
    }
}
