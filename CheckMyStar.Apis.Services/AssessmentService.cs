using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class AssessmentService(IAssessmentBusForService assessmentBusForService) : IAssessmentService
    {
        public async Task<AssessmentsResponse> GetAssessments(CancellationToken ct)
        {
            var assessments = await assessmentBusForService.GetAssessments(ct);

            return assessments;
        }

        public async Task<AssessmentResponse> AddAssessment(AssessmentSaveRequest request, CancellationToken ct)
        {
            var assessment = await assessmentBusForService.AddAssessment(request, ct);

            return assessment;
        }

        public async Task<BaseResponse> DeleteAssessment(AssessmentDeleteRequest request, CancellationToken ct)
        {
            var response = await assessmentBusForService.DeleteAssessment(request, ct);

            return response;
        }
    }
}
