using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class ActivitiesResponse : BaseResponse
    {
        public List<ActivityModel>? Activities { get; set; }
    }
}
