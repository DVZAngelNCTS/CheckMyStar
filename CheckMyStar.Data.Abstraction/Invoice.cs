using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Invoice
{
    public int Identifier { get; set; }

    public string Number { get; set; } = null!;

    public decimal Amount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? DueDate { get; set; }
}
