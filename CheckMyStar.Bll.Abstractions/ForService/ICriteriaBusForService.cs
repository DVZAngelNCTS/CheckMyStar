using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface ICriteriaBusForService
    {
        Task<StarCriteriaStatusResponse> GetStarCriteriaStatus(CancellationToken ct);
        Task<StarCriteriaDetailsResponse> GetStarCriteriaDetails(CancellationToken ct);
    }
}
