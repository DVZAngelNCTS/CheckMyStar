using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Society
{
    public int Identifier { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? AddressIdentifier { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
