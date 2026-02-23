using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAssessmentDal
    {
        Task<AssessmentsResult> GetAssessments(CancellationToken ct);
        Task<AssessmentResult> CreateAssessment(Assessment assessment, List<AssessmentCriterion> criteria, CancellationToken ct);
        Task<AssessmentResult> UpdateAssessment(Assessment assessment, List<AssessmentCriterion> criteria, CancellationToken ct);
        Task<AssessmentResult> DeleteAssessment(int id, CancellationToken ct);
    }
}
