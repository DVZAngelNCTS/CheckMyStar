namespace CheckMyStar.Bll.Models
{
    public class InvoiceLineModel
    {
        public int Identifier { get; set; }
        public int InvoiceIdentifier { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal UnitPriceHT { get; set; }
        public decimal VATRate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
