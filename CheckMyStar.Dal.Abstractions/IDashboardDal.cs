using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IDashboardDal
    {
        Task<DashboardResult> GetDashboard(CancellationToken ct);
    }
}
