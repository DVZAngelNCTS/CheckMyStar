using CheckMyStar.Apis.Services.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface ICriteriaService
    {
        Task<StarCriteriaDetailsResponse> GetStarCriteriaDetails(CancellationToken ct);
        Task<StarCriteriaStatusResponse> GetStarCriteriaStatus(CancellationToken ct);
        Task<BaseResponse> AddCriterion(CriterionSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteCriterion(int criterionId, CancellationToken ct);
    }
}
