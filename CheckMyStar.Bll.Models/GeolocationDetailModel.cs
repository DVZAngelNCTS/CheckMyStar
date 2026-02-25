using System.Text.Json.Serialization;

namespace CheckMyStar.Bll.Models
{
    public class GeolocationDetailModel
    {
        [JsonPropertyName("label")]
        public string? Label { get; set; }
        [JsonPropertyName("score")]
        public double Score { get; set; }
        [JsonPropertyName("housenumber")]
        public string? Number { get; set; }
        [JsonPropertyName("id")]
        public string? Identifier { get; set; }
        [JsonPropertyName("banId")]
        public string? BanIdentifier { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("postcode")]
        public string? PostalCode { get; set; }
        [JsonPropertyName("citycode")]
        public string? CityCode { get; set; }
        [JsonPropertyName("x")]
        public double Latitude { get; set; }
        [JsonPropertyName("y")]
        public double Longitude { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("context")]
        public string? Context { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("importance")]
        public double Importance { get; set; }
        [JsonPropertyName("street")]
        public string? Street { get; set; }
        [JsonPropertyName("_type")]
        public string? _Type { get; set; }
    }
}
