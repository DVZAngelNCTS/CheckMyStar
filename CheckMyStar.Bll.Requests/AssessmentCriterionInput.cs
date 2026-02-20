namespace CheckMyStar.Bll.Requests
{
    public class AssessmentCriterionInput
    {
        public int CriterionId { get; set; }
        public int Points { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsValidated { get; set; }
        public string? Comment { get; set; }
    }
}
