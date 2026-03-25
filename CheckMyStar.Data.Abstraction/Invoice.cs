using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Invoice
{
    public int Identifier { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public int? QuoteIdentifier { get; set; }

    public int? ClientUserIdentifier { get; set; }

    public int? ClientAddressIdentifier { get; set; }

    public int? CompanySocietyIdentifier { get; set; }

    public int? CompanyAddressIdentifier { get; set; }

    public DateTime InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    public decimal TotalAmountHT { get; set; }

    public decimal TotalVATAmount { get; set; }

    public decimal TotalAmountTTC { get; set; }

    public int? PaymentStatusIdentifier { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public ICollection<InvoiceLine> InvoiceLines { get; set; } = [];
}
