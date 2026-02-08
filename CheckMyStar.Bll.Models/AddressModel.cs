namespace CheckMyStar.Bll.Models
{
    public class AddressModel
    {
        public int Identifier { get; set; }
        public string Number { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string? Region { get; set; }
        public CountryModel Country { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}