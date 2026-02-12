using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICriteresDal
    {
        Task<StarCriteriasResult> GetStarCriterias(CancellationToken ct);
        Task<StarCriteriasDetailResult> GetStarCriteriaDetails(CancellationToken ct);
        Task<int> CreateCriterionAsync(string description, decimal basePoints, CancellationToken ct);
        Task AddStarLevelCriterionAsync(int starLevelId, int criterionId, string typeCode, CancellationToken ct);
        Task<bool> UpdateCriterionAsync(int criterionId, string description, decimal basePoints, CancellationToken ct);
        Task DeleteStarLevelCriteriaAsync(int criterionId, CancellationToken ct);
    }
}
