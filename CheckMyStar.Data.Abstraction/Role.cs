using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Role
{
    public int Identifier { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}
