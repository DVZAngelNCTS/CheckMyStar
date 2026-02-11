using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface ICriteriaBus
    {
        Task<StarCriteriaStatusResponse> GetCriteriaStatus(CancellationToken ct);
        Task<StarCriteriaDetailsResponse> GetCriteriaDetails(CancellationToken ct);
    }
}
