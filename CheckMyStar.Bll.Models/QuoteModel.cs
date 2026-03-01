namespace CheckMyStar.Bll.Models
{
    public class QuoteModel
    {
        public int Identifier { get; set; }
        public string? Reference { get; set; }
        public decimal Amount { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate {  get; set; }
    }
}
