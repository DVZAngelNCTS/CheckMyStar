using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class AccommodationType
{
    public int Identifier { get; set; }

    public string Label { get; set; } = null!;

    public string? Description { get; set; }
}
