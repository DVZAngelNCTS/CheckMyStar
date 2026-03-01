using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Quote
{
    public int Identifier { get; set; }

    public string Reference { get; set; } = null!;

    public decimal Amount { get; set; }

    public bool IsAccepted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
