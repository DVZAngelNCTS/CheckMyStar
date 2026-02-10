using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class StarLevelCriterion
{
    public byte StarLevelId { get; set; }

    public int CriterionId { get; set; }

    public string TypeCode { get; set; } = null!;
}
