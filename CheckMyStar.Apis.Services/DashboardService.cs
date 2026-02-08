using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class DashboardService(IDashboardBusForService dashboardBusForService) : IDashboardService
    {
        public async Task<DashboardResponse> GetDashboard(CancellationToken ct)
        {
            var dashboard = await dashboardBusForService.GetDashboard(ct);

            return dashboard;
        }
    }
}
