using System;

namespace CheckMyStar.Data;

public partial class Quote
{
    public int Identifier { get; set; }

    public decimal Amount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ValidUntilDate { get; set; }

    public string? Description { get; set; }
}
