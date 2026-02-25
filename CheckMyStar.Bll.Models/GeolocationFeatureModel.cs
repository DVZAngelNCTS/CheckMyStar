using System.Text.Json.Serialization;

namespace CheckMyStar.Bll.Models
{
    public class GeolocationFeatureModel
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("geometry")]
        public GeoLocationGeometryModel? Geometry { get; set; }
        [JsonPropertyName("properties")]
        public GeolocationDetailModel? Detail { get; set; }
    }
}
