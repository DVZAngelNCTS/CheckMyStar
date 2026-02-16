using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class User
{
    public int Identifier { get; set; }

    public int CivilityIdentifier { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public int? SocietyIdentifier { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;

    public int RoleIdentifier { get; set; }

    public int? AddressIdentifier { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
