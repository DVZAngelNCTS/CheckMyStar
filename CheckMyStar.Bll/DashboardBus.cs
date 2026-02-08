using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;

namespace CheckMyStar.Bll
{
    public partial class DashboardBus(IDashboardDal dashboardDal, IMapper mapper) : IDashboardBus
    {
        public async Task<DashboardResponse> GetDashboardBack(CancellationToken ct)
        {
            var dashboard = await dashboardDal.GetDashboard(ct);

            return mapper.Map<DashboardResponse>(dashboard);
        }
    }
}
