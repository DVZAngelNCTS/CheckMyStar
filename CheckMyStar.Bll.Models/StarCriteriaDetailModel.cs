namespace CheckMyStar.Bll.Models
{
    public class StarCriteriaDetailModel
    {
        public int CriterionId { get; set; }
        public string? Description { get; set; }
        public decimal BasePoints { get; set; }
        public string? TypeCode { get; set; }
        public string? TypeLabel { get; set; }
    }
}
