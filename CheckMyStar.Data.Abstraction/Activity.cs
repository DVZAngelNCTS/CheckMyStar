using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Activity
{
    public int Identifier { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public int User { get; set; }

    public bool IsSuccess { get; set; }
}
