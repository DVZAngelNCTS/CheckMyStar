using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class DashboardResponse : BaseResponse
    {
        public DashboardModel? Dashboard { get; set; }
    }
}
