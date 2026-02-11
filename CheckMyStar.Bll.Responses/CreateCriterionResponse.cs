namespace CheckMyStar.Bll.Responses
{
    public class CreateCriterionResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public int CriterionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePoints { get; set; }
    }
}
