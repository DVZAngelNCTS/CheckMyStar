using System;

namespace CheckMyStar.Data;

public partial class QuoteLine
{
    public int Identifier { get; set; }

    public int QuoteIdentifier { get; set; }

    public string Description { get; set; } = null!;

    public decimal Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public decimal UnitPriceHT { get; set; }

    public decimal VATRate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
