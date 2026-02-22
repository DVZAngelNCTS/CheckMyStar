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

        public Task<ActivitiesResponse> GetActivities(ActivitiesGetRequest request, CancellationToken ct)
        {
            return this.GetActivities(request.LastName, request.FirstName, request.Description, request.CreatedDate, request.IsSuccess, ct);
        }
    }
}
