namespace CheckMyStar.Bll.Models
{
    public class GeolocationAddressModel
    {
        public string? Label { get; set; }
        public string? Number { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? Score { get; set; }
    }
}
