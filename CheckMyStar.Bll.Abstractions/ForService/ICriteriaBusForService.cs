using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface ICriteriaBusForService
    {
        Task<StarCriteriaStatusResponse> GetStarCriteriaStatus(CancellationToken ct);
        Task<StarCriteriaDetailsResponse> GetStarCriteriaDetails(CancellationToken ct);
        Task<CreateCriterionResponse> CreateCriterionAsync(CreateCriterionRequest request, CancellationToken ct);
        Task<UpdateCriterionResponse> UpdateCriterionAsync(UpdateCriterionRequest request, CancellationToken ct);
    }
}
