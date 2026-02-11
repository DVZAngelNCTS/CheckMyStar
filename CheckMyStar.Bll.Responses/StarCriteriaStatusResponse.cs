using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class StarCriteriaStatusResponse : BaseResponse
    {
        public List<StarCriteriaStatusModel>? StarCriterias { get; set; }
    }
}
