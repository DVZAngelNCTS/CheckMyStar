using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Apis.Services.Models;
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

        public async Task<CreateCriterionResponse> CreateCriterionAsync(CreateCriterionModel model, CancellationToken ct)
        {
            var request = new CreateCriterionRequest
            {
                Description = model.Description,
                BasePoints = model.BasePoints,
                StarLevels = model.StarLevels
                    .Select(sl => new StarLevelCriterionRequest
                    {
                        StarLevelId = sl.StarLevelId,
                        TypeCode = sl.TypeCode
                    })
                    .ToList()
            };

            var response = await criteriaBusForService.CreateCriterionAsync(request, ct);
            return response;
        }
    }
}
