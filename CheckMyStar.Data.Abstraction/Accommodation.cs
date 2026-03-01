using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Accommodation
{
    public int Identifier { get; set; }

    public string AccommodationName { get; set; } = null!;

    public string? AccommodationPhone { get; set; }

    public int AccommodationTypeIdentifier { get; set; }

    public int? AccommodationCurrentStar { get; set; }

    public int AddressIdentifier { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
