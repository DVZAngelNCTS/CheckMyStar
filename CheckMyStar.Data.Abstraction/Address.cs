using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Address
{
    public int Identifier { get; set; }

    public string Number { get; set; } = null!;

    public string AddressLine { get; set; } = null!;

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string? Region { get; set; }

    public int CountryIdentifier { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
