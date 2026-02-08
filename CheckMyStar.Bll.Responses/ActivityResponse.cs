using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class ActivityResponse : BaseResponse
    {
        public ActivityModel? Activity { get; set; }
    }
}
