using CheckMyStar.Bll.Abstractions.ForService;
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
    }
}
