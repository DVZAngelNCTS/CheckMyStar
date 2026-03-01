namespace CheckMyStar.Bll.Models
{
    public class InvoiceModel
    {
        public int Identifier { get; set; }
        public string? Number { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
