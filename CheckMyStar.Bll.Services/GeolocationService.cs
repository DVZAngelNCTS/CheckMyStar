using System.Text.Json;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Services
{
    public partial class GeolocationService : IGeolocationService
    {
        public async Task<GeolocationResponse> Search(string address, CancellationToken ct)
        {
            GeolocationResponse geolocationResponse = new GeolocationResponse();

            try
            {
                HttpClient httpClient = new HttpClient();

                var url = $"https://data.geopf.fr/geocodage/search?q={Uri.EscapeDataString(address)}&autocomplete=1&index=address&limit=10&returntruegeometry=false";

                var response = await httpClient.GetAsync(url, ct);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(ct);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var geolocation = JsonSerializer.Deserialize<GeolocationModel>(content, options);

                    var features = geolocation?.Features?.ToList();

                    if (features != null)
                    {
                        List<GeolocationAddressModel> geolocationAddresses = new List<GeolocationAddressModel>();

                        foreach (var feature in features)
                        {
                            var geolocationAddress = new GeolocationAddressModel()
                            {
                                Label = feature.Detail?.Label,
                                Number = feature.Detail?.Number,
                                Street = feature.Detail?.Street,
                                ZipCode = feature.Detail?.PostalCode,
                                City = feature.Detail?.City,
                                Longitude = feature.Geometry?.Coordinates?[0],
                                Latitude = feature.Geometry?.Coordinates?[1],
                                Score = feature.Detail?.Score
                            };

                            geolocationAddresses.Add(geolocationAddress);
                        }

                        geolocationResponse.IsSuccess = true;
                        geolocationResponse.Addresses = geolocationAddresses;
                        geolocationResponse.Message = "L'addresse à été trouvée";
                    }
                    else
                    {
                        geolocationResponse.IsSuccess = true;
                        geolocationResponse.Message = "L'addresse n'existe pas";
                    }
                }
            }
            catch (Exception ex)
            {
                geolocationResponse.IsSuccess = false;
                geolocationResponse.Message = "Impossible de trouver l'adresse " + ex.Message;

            }

            return geolocationResponse;
        }
    }
}
