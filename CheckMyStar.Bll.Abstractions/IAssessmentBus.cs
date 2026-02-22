using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IAssessmentBus
    {
        Task<AssessmentsResponse> GetAllAssessments(CancellationToken ct);
        Task<AssessmentResponse> AddAssessment(AssessmentModel assessmentModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteAssessment(int identifier, int currentUser, CancellationToken ct);
    }
}
