namespace CheckMyStar.Dal.Models
{
    public class StarCriteriaDetail
    {
        public int Rating { get; set; }
        public string StarLabel { get; set; } = string.Empty;
        public int CriterionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int BasePoints { get; set; }
        public string TypeCode { get; set; } = string.Empty;
        public string TypeLabel { get; set; } = string.Empty;
    }
}
