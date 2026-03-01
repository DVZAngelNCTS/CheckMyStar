using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Appointment
{
    public int Identifier { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string? Location { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
