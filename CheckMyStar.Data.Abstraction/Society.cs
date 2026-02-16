namespace CheckMyStar.Data.Abstractions;

public partial class Society
{
    public int Identifier { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public int? AddressIdentifier { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    // Navigation property (si Address existe)
    public virtual Address? Address { get; set; }
}