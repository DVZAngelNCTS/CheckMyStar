using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Services
{
    public partial class GeolocationService : IGeolocationBusForService
    {
        public Task<GeolocationResponse> SearchAddress(GeolocationGetRequest request, CancellationToken ct)
        {
            return this.Search(request.Address!, ct);
        }
    }
}
