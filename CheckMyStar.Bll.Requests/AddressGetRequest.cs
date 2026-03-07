namespace CheckMyStar.Bll.Requests
{
    public class AddressGetRequest
    {
        public string? Number { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Region { get; set; }
        public int? CountryIdentifier { get; set; }
    }
}
