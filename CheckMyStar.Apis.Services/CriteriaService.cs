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

        public async Task<UpdateCriterionResponse> UpdateCriterionAsync(UpdateCriterionModel model, CancellationToken ct)
        {
            var request = new UpdateCriterionRequest
            {
                CriterionId = model.CriterionId,
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

            var response = await criteriaBusForService.UpdateCriterionAsync(request, ct);
            return response;
        }
    }
}