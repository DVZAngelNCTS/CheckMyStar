using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Country
{
    public int Identifier { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;
}
