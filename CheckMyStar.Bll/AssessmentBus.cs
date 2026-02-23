using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public class AssessmentBus(IAssessmentDal assessmentDal, IMapper mapper) : IAssessmentBus, IAssessmentBusForService
    {
        public async Task<AssessmentsResponse> GetAssessments(CancellationToken ct)
        {
            var result = await assessmentDal.GetAssessments(ct);

            var response = new AssessmentsResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                Assessments = result.Assessments?.Select(a => mapper.Map<AssessmentModel>(a)).ToList()
            };

            return response;
        }

        public async Task<AssessmentResponse> CreateAssessment(AssessmentCreateRequest request, CancellationToken ct)
        {
            var assessment = new Assessment
            {
                FolderIdentifier = request.FolderIdentifier,
                TargetStarLevel = request.TargetStarLevel,
                Capacity = request.Capacity,
                NumberOfFloors = request.NumberOfFloors,
                IsWhiteZone = request.IsWhiteZone,
                IsDromTom = request.IsDromTom,
                IsHighMountain = request.IsHighMountain,
                IsBuildingClassified = request.IsBuildingClassified,
                IsStudioNoLivingRoom = request.IsStudioNoLivingRoom,
                IsParkingImpossible = request.IsParkingImpossible,
                TotalArea = request.TotalArea,
                NumberOfRooms = request.NumberOfRooms,
                TotalRoomsArea = request.TotalRoomsArea,
                SmallestRoomArea = request.SmallestRoomArea,
                IsComplete = request.IsComplete
            };

            var criteria = request.Criteria.Select(c => new AssessmentCriterion
            {
                CriterionId = c.CriterionId,
                Points = c.Points,
                Status = c.Status,
                IsValidated = c.IsValidated,
                Comment = c.Comment
            }).ToList();

            var result = await assessmentDal.CreateAssessment(assessment, criteria, ct);

            var response = new AssessmentResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                Assessment = result.Assessment != null ? mapper.Map<AssessmentModel>(result.Assessment) : null
            };

            return response;
        }

        public async Task<AssessmentResponse> UpdateAssessment(AssessmentUpdateRequest request, CancellationToken ct)
        {
            var assessment = new Assessment
            {
                Identifier = request.Identifier,
                FolderIdentifier = request.FolderIdentifier,
                TargetStarLevel = request.TargetStarLevel,
                Capacity = request.Capacity,
                NumberOfFloors = request.NumberOfFloors,
                IsWhiteZone = request.IsWhiteZone,
                IsDromTom = request.IsDromTom,
                IsHighMountain = request.IsHighMountain,
                IsBuildingClassified = request.IsBuildingClassified,
                IsStudioNoLivingRoom = request.IsStudioNoLivingRoom,
                IsParkingImpossible = request.IsParkingImpossible,
                TotalArea = request.TotalArea,
                NumberOfRooms = request.NumberOfRooms,
                TotalRoomsArea = request.TotalRoomsArea,
                SmallestRoomArea = request.SmallestRoomArea,
                IsComplete = request.IsComplete
            };

            var criteria = request.Criteria.Select(c => new AssessmentCriterion
            {
                CriterionId = c.CriterionId,
                Points = c.Points,
                Status = c.Status,
                IsValidated = c.IsValidated,
                Comment = c.Comment
            }).ToList();

            var result = await assessmentDal.UpdateAssessment(assessment, criteria, ct);

            var response = new AssessmentResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                Assessment = result.Assessment != null ? mapper.Map<AssessmentModel>(result.Assessment) : null
            };

            return response;
        }

        public async Task<AssessmentResponse> DeleteAssessment(int id, CancellationToken ct)
        {
            var result = await assessmentDal.DeleteAssessment(id, ct);

            var response = new AssessmentResponse
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message
            };

            return response;
        }
    }
}
