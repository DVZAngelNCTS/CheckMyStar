using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAssessmentResultDal
    {
        Task<AssessmentResultResult> GetNextIdentifier(CancellationToken ct);
        Task<AssessmentResultResult> AddAssessmentResult(Data.AssessmentResult assessmentResult, CancellationToken ct);
        Task<AssessmentResultResult> UpdateAssessmentResult(Data.AssessmentResult assessmentResult, CancellationToken ct);
        Task<AssessmentsResultResult> GetAssessmentResultsByFolder(int folderIdentifier, CancellationToken ct);
    }
}
