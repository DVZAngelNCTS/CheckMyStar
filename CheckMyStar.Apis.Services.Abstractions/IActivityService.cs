using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IActivityService
    {
        Task<ActivitiesResponse> GetActivities(ActivityGetRequest request, CancellationToken ct);
    }
}
