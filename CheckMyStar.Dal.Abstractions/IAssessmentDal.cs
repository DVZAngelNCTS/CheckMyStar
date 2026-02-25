using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAssessmentDal
    {
        Task<AssessmentResult> GetAssessment(int identifier, CancellationToken ct);
        Task<AssessmentsResult> GetAssessments(CancellationToken ct);
        Task<AssessmentResult> GetAssessmentByFolder(int folderIdentifier, CancellationToken ct);
        Task<AssessmentCriteriaResult> GetAssessmentCriteria(int assessmentIdentifier, CancellationToken ct);
        Task<AssessmentResult> AddAssessment(Assessment assessment, List<AssessmentCriterion> criteria, CancellationToken ct);
        Task<AssessmentResult> UpdateAssessment(Assessment assessment, List<AssessmentCriterion> criteria, CancellationToken ct);
        Task<BaseResult> DeleteAssessment(Assessment assessment, CancellationToken ct);
    }
}
