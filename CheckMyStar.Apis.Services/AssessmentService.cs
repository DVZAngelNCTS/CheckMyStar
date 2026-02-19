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

        public async Task<AssessmentResponse> CreateAssessment(AssessmentCreateRequest request, CancellationToken ct)
        {
            var assessment = await assessmentBusForService.CreateAssessment(request, ct);

            return assessment;
        }

        public async Task<AssessmentResponse> DeleteAssessment(int id, CancellationToken ct)
        {
            var response = await assessmentBusForService.DeleteAssessment(id, ct);

            return response;
        }
    }
}
