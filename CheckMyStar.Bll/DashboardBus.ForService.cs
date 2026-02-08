using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class DashboardBus : IDashboardBusForService
    {
        public Task<DashboardResponse> GetDashboard(CancellationToken ct)
        {
            return this.GetDashboardBack(ct);
        }
    }
}
