using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IDashboardBusForService
    {
        Task<DashboardResponse> GetDashboard(CancellationToken ct);
    }
}
