using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IAssessmentService
    {
        Task<AssessmentsResponse> GetAssessments(CancellationToken ct);
        Task<AssessmentResponse> CreateAssessment(AssessmentCreateRequest request, CancellationToken ct);
        Task<AssessmentResponse> DeleteAssessment(int id, CancellationToken ct);
    }
}
