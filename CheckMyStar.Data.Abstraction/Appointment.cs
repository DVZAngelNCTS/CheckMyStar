using System;

namespace CheckMyStar.Data;

public partial class Appointment
{
    public int Identifier { get; set; }

    public DateTime AppointmentDate { get; set; }

    public int? AddressIdentifier { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
