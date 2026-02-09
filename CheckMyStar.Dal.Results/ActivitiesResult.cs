using CheckMyStar.Dal.Models;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Results
{
    public class ActivitiesResult : BaseResult
    {
        public List<UserActivity>? Activities { get; set; }
    }
}
