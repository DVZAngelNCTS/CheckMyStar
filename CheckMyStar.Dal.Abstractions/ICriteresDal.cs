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
        Task<BaseResult> DeleteStarLevelCriterionByCriterionId(int criterionId, CancellationToken ct);
        Task<BaseResult> DeleteCriterion(int criterionId, CancellationToken ct);
        Task<BaseResult> UpdateCriterion(Criterion criterion, CancellationToken ct);
        Task<BaseResult> UpdateStarLevelCriterionType(int criterionId, byte starLevelId, string typeCode, CancellationToken ct);
        Task<List<byte>> GetStarLevelIdsByCriterionId(int criterionId, CancellationToken ct);
        Task<BaseResult> UpdateStarLevelLastUpdate(byte starLevelId, CancellationToken ct);
    }
}
