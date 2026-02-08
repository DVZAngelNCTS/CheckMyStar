using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IDashboardService
    {
        Task<DashboardResponse> GetDashboard(CancellationToken ct);
    }
}
