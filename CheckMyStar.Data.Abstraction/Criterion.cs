using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Criterion
{
    public int CriterionId { get; set; }

    public string Description { get; set; } = null!;

    public decimal BasePoints { get; set; }
}
