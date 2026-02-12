using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface ICriteriaBus
    {
        Task<StarCriteriaStatusResponse> GetCriteriaStatus(CancellationToken ct);
        Task<StarCriteriaDetailsResponse> GetCriteriaDetails(CancellationToken ct);
        Task<CreateCriterionResponse> CreateCriterionAsync(CreateCriterionRequest request, CancellationToken ct);
        Task<UpdateCriterionResponse> UpdateCriterionAsync(UpdateCriterionRequest request, CancellationToken ct);

    }
}
