using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class ActivityService(IActivityBusForService activityBusForService) : IActivityService
    {
        public async Task<ActivitiesResponse> GetActivities(ActivityGetRequest request, CancellationToken ct)
        {
            var activities = await activityBusForService.GetActivities(request, ct);

            return activities;
        }

        public async Task<ActivitiesResponse> GetActivities(ActivitiesGetRequest request, CancellationToken ct)
        {
            var activities = await activityBusForService.GetActivities(request, ct);

            return activities;
        }
    }
}
