using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAssessmentResultDal
    {
        Task<AssessmentResultEntityResult> GetNextIdentifier(CancellationToken ct);
        Task<AssessmentResultEntityResult> AddAssessmentResult(AssessmentResultEntity assessmentResult, CancellationToken ct);
        Task<AssessmentResultEntityResult> UpdateAssessmentResult(AssessmentResultEntity assessmentResult, CancellationToken ct);
        Task<AssessmentResultEntitiesResult> GetAssessmentResultsByFolder(int folderIdentifier, CancellationToken ct);
    }
}
