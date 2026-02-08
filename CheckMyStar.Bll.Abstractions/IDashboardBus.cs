using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IDashboardBus
    {
        Task<DashboardResponse> GetDashboardBack(CancellationToken ct);
    }
}
