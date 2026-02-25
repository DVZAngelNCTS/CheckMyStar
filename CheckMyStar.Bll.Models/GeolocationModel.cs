using System.Text.Json.Serialization;

namespace CheckMyStar.Bll.Models
{
    public class GeolocationModel
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("features")]
        public List<GeolocationFeatureModel>? Features { get; set; }
    }
}
