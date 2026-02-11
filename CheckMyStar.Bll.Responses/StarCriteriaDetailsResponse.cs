using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class StarCriteriaDetailsResponse : BaseResponse
    {
        public List<StarCriteriaModel>? StarCriterias { get; set; }
    }
}
