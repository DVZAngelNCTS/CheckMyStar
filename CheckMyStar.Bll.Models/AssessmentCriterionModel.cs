namespace CheckMyStar.Bll.Models
{
    public class AssessmentCriterionModel
    {
        public int AssessmentIdentifier { get; set; }
        public int CriterionId { get; set; }
        public int Points { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsValidated { get; set; }
        public string? Comment { get; set; }
    }
}
