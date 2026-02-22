using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class CriterionUpdateRequest
    {
        public StarCriterionModel? Criterion { get; set; }
        public StarLevelModel? StarLevel { get; set; }
        public StarLevelCriterionModel? StarLevelCriterion { get; set; }
    }
}