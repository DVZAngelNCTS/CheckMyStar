using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class AssessmentResultService(IAssessmentResultBusForService assessmentResultBusForService) : IAssessmentResultService
    {
        public async Task<AssessmentResultResponse> GetNextIdentifier(CancellationToken ct)
        {
            var response = await assessmentResultBusForService.GetNextIdentifier(ct);

            return response;
        }

        public async Task<AssessmentResultResponse> AddAssessmentResult(AssessmentResultSaveRequest request, CancellationToken ct)
        {
            var response = await assessmentResultBusForService.AddAssessmentResult(request, ct);

            return response;
        }

        public async Task<AssessmentResultResponse> UpdateAssessmentResult(AssessmentResultSaveRequest request, CancellationToken ct)
        {
            var response = await assessmentResultBusForService.UpdateAssessmentResult(request, ct);

            return response;
        }

        public async Task<AssessmentResultsResponse> GetAssessmentResultsByFolder(AssessmentResultGetByFolderRequest request, CancellationToken ct)
        {
            var response = await assessmentResultBusForService.GetAssessmentResultsByFolder(request, ct);

            return response;
        }
    }
}
