namespace CheckMyStar.Bll.Models
{
    public class QuoteModel
    {
        public int Identifier { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ValidUntilDate { get; set; }
        public string? Description { get; set; }
    }
}
