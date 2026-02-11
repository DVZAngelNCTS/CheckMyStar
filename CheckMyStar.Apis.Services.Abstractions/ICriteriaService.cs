using CheckMyStar.Apis.Services.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface ICriteriaService
    {
        Task<StarCriteriaDetailsResponse> GetStarCriteriaDetails(CancellationToken ct);
        Task<StarCriteriaStatusResponse> GetStarCriteriaStatus(CancellationToken ct);
        Task<CreateCriterionResponse> CreateCriterionAsync(CreateCriterionModel model, CancellationToken ct);
    }
}
