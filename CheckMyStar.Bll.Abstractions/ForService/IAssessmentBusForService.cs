using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IAssessmentBusForService
    {
        Task<AssessmentsResponse> GetAssessments(CancellationToken ct);
        Task<AssessmentResponse> CreateAssessment(AssessmentCreateRequest request, CancellationToken ct);
        Task<AssessmentResponse> DeleteAssessment(int id, CancellationToken ct);
    }
}
