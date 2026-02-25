using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IAssessmentResultBusForService
    {
        Task<AssessmentResultResponse> GetNextIdentifier(CancellationToken ct);
        Task<AssessmentResultResponse> AddAssessmentResult(AssessmentResultSaveRequest request, CancellationToken ct);
        Task<AssessmentResultResponse> UpdateAssessmentResult(AssessmentResultSaveRequest request, CancellationToken ct);
        Task<AssessmentResultsResponse> GetAssessmentResultsByFolder(AssessmentResultGetByFolderRequest request, CancellationToken ct);
    }
}
