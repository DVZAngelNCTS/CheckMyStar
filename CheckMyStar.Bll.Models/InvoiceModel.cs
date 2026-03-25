namespace CheckMyStar.Bll.Models
{
    public class InvoiceModel
    {
        public int Identifier { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public int? QuoteIdentifier { get; set; }
        public int? ClientUserIdentifier { get; set; }
        public UserModel? ClientUser { get; set; }
        public int? ClientAddressIdentifier { get; set; }
        public AddressModel? ClientAddress { get; set; }
        public int? CompanySocietyIdentifier { get; set; }
        public SocietyModel? CompanySociety { get; set; }
        public int? CompanyAddressIdentifier { get; set; }
        public AddressModel? CompanyAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmountHT { get; set; }
        public decimal TotalVATAmount { get; set; }
        public decimal TotalAmountTTC { get; set; }
        public int? PaymentStatusIdentifier { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<InvoiceLineModel> InvoiceLines { get; set; } = new List<InvoiceLineModel>();
    }
}
