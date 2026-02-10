using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class StarLevel
{
    public byte StarLevelId { get; set; }

    public string Label { get; set; } = null!;
}
