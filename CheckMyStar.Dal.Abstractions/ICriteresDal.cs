using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICriteresDal
    {
        Task<CriterionResult> GetNextIdentifier(CancellationToken ct);
        Task<StarCriteriasResult> GetStarCriterias(CancellationToken ct);
        Task<StarCriteriasDetailResult> GetStarCriteriaDetails(CancellationToken ct);
        Task<BaseResult> AddStarLevelCriterion(StarLevelCriterion starLevelCriterion, CancellationToken ct);
        Task<BaseResult> AddCriterion(Criterion criterion, CancellationToken ct);
    }
}
