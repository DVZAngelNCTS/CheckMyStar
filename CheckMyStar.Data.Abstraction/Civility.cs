using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Civility
{
    public int Identifier { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}
