using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAssessmentDal
    {
        Task<AssessmentResult> GetAssessment(int identifier, CancellationToken ct);
        Task<AssessmentsResult> GetAssessments(CancellationToken ct);
        Task<AssessmentResult> GetAssessmentByFolder(int folderIdentifier, CancellationToken ct);
        Task<AssessmentCriteriaResult> GetAssessmentCriteria(int assessmentIdentifier, CancellationToken ct);
        Task<AssessmentResult> AddAssessment(Data.Assessment assessment, List<Data.AssessmentCriterion> criteria, CancellationToken ct);
        Task<AssessmentResult> UpdateAssessment(Data.Assessment assessment, List<Data.AssessmentCriterion> criteria, CancellationToken ct);
        Task<BaseResult> DeleteAssessment(Data.Assessment assessment, CancellationToken ct);
    }
}
