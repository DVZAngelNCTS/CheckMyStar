using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface ICriteriaBusForService
    {
        Task<StarCriteriaStatusResponse> GetStarCriteriaStatus(CancellationToken ct);
        Task<StarCriteriaDetailsResponse> GetStarCriteriaDetails(CancellationToken ct);
        Task<BaseResponse> AddCriterion(CriterionSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteCriterion(int criterionId, CancellationToken ct);
    }
}
