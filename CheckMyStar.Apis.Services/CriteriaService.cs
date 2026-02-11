using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
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
    }
}
