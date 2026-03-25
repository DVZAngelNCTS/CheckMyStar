namespace CheckMyStar.Bll.Models
{
    public class SocietyModel
    {
        public int Identifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? LogoPath { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? SiretCode { get; set; }
        public string? VatNumber { get; set; }
        public string? LegalInformation { get; set; }
        public AddressModel? Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}