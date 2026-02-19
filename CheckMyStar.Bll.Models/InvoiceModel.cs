namespace CheckMyStar.Bll.Models
{
    public class InvoiceModel
    {
        public int Identifier { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsPaid { get; set; }
    }
}
