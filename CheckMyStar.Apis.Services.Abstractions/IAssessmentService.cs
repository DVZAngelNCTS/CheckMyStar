using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IAssessmentService
    {
        Task<AssessmentsResponse> GetAssessments(CancellationToken ct);
        Task<AssessmentResponse> AddAssessment(AssessmentSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteAssessment(AssessmentDeleteRequest request, CancellationToken ct);
    }
}
