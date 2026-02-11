namespace CheckMyStar.Bll.Requests
{
    public class StarLevelCriterionRequest
    {
        public int StarLevelId { get; set; }
        public string TypeCode { get; set; } = string.Empty;
    }

    public class CreateCriterionRequest
    {
        public string Description { get; set; } = string.Empty;
        public decimal BasePoints { get; set; }

        public List<StarLevelCriterionRequest> StarLevels { get; set; } = new();
    }
}
