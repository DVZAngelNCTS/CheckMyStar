using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICriteresDal
    {
        Task<StarCriteriasResult> GetStarCriterias(CancellationToken ct);
        Task<StarCriteriasDetailResult> GetStarCriteriaDetails(CancellationToken ct);
    }
}
