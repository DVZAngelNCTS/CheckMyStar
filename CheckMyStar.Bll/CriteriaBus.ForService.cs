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
            var user = userContext.CurrentUser.Identifier;

            return this.AddCriterion(request.StarCriterion!, request.StarLevelCriterion!, user, ct);
        }

        public Task<BaseResponse> DeleteCriterion(CriterionDeleteRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteCriterion(request.Identifier, user, ct);
        }

        public Task<BaseResponse> UpdateCriterion(CriterionUpdateRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateCriterion(request, user, ct);
        }
    }
}
