using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class ActivityBus : IActivityBusForService
    {
        public Task<ActivitiesResponse> GetActivities(ActivityGetRequest request, CancellationToken ct)
        {
            return this.GetActivities(request.NumberDays, ct);
        }
    }
}
