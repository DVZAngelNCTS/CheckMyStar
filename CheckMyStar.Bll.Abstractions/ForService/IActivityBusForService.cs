using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IActivityBusForService
    {
        Task<ActivitiesResponse> GetActivities(ActivityGetRequest request, CancellationToken ct);
        Task<ActivitiesResponse> GetActivities(ActivitiesGetRequest request, CancellationToken ct);
    }
}
