using System;

namespace CheckMyStar.Data;

public partial class Appointment
{
    public int Identifier { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedDate { get; set; }
}
