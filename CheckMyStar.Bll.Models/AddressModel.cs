namespace CheckMyStar.Bll.Models
{
    public class AddressModel
    {
        public int Identifier { get; set; }
        public string? Number { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Region { get; set; }
        public CountryModel? Country { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}