using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class CriterionSaveRequest
    {
        public StarLevelCriterionModel? StarLevelCriterion { get; set; }
        public StarCriterionModel? StarCriterion { get; set; }
    }
}
