using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IAssessmentResultBus
    {
        Task<AssessmentResultResponse> GetNextIdentifier(CancellationToken ct);
        Task<AssessmentResultResponse> AddAssessmentResult(AssessmentResultModel assessmentResultModel, int currentUser, CancellationToken ct);
        Task<AssessmentResultResponse> UpdateAssessmentResult(AssessmentResultModel assessmentResultModel, int currentUser, CancellationToken ct);
        Task<AssessmentResultsResponse> GetAssessmentResultsByFolder(int folderIdentifier, CancellationToken ct);
    }
}
