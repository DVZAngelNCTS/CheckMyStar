using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class CriteriaService(ICriteriaBusForService criteriaBusForService) : ICriteriaService
    {
        public async Task<StarCriteriaDetailsResponse> GetStarCriteriaDetails(CancellationToken ct)
        {
            var criterias = await criteriaBusForService.GetStarCriteriaDetails(ct);

            return criterias;
        }

        public async Task<StarCriteriaStatusResponse> GetStarCriteriaStatus(CancellationToken ct)
        {
            var criterias = await criteriaBusForService.GetStarCriteriaStatus(ct);

            return criterias;
        }
        public async Task<BaseResponse> AddCriterion(CriterionSaveRequest request, CancellationToken ct)
        {
            var response = await criteriaBusForService.AddCriterion(request, ct);

            return response;
        }

        public async Task<BaseResponse> DeleteCriterion(int criterionId, CancellationToken ct)
        {
            return await criteriaBusForService.DeleteCriterion(criterionId, ct);
        }
    }
}