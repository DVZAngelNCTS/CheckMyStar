using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class CriteriaBus : ICriteriaBusForService
    {
        public Task<StarCriteriaStatusResponse> GetStarCriteriaStatus(CancellationToken ct)
        {
            return this.GetCriteriaStatus(ct);
        }

        public Task<StarCriteriaDetailsResponse> GetStarCriteriaDetails(CancellationToken ct)
        {
            return this.GetCriteriaDetails(ct);
        }

        public Task<BaseResponse> AddCriterion(CriterionSaveRequest request, CancellationToken ct)
        {
            return this.AddCriterion(request.StarCriterion!, request.StarLevelCriterion!, ct);
        }

        Task<BaseResponse> ICriteriaBusForService.DeleteCriterion(int criterionId, CancellationToken ct)
        {
            return this.DeleteCriterion(criterionId, ct);
        }
    }
}
