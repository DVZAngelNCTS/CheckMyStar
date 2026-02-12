namespace CheckMyStar.Bll.Requests
{
    public class UpdateCriterionRequest
    {
        public int CriterionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePoints { get; set; }
        public List<StarLevelCriterionRequest> StarLevels { get; set; } = new();
    }
}