using System.Text.Json.Serialization;

namespace CheckMyStar.Bll.Models
{
    public class GeoLocationGeometryModel
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("coordinates")]
        public List<double>? Coordinates { get; set; }
    }
}
