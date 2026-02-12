namespace CheckMyStar.Apis.Services.Models
{
    public class UpdateCriterionModel
    {
        public int CriterionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePoints { get; set; }
        public List<StarLevelCriterionModel> StarLevels { get; set; } = new();
    }
}