using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface ICriteriaBus
    {
        Task<StarCriteriaStatusResponse> GetCriteriaStatus(CancellationToken ct);
        Task<StarCriteriaDetailsResponse> GetCriteriaDetails(CancellationToken ct);
        Task<BaseResponse> AddCriterion(StarCriterionModel starCriterionModel, StarLevelCriterionModel starLevelCriterionModel, CancellationToken ct);
    }
}
