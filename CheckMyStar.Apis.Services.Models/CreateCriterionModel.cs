namespace CheckMyStar.Apis.Services.Models
{
    public class StarLevelCriterionModel
    {
        public int StarLevelId { get; set; }
        public string TypeCode { get; set; } = "";
    }

    public class CreateCriterionModel
    {
        public string Description { get; set; } = string.Empty;
        public decimal BasePoints { get; set; }

        public List<StarLevelCriterionModel> StarLevels { get; set; } = new();
    }
}
