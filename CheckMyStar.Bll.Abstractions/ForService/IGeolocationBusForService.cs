using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IGeolocationBusForService
    {
        Task<GeolocationResponse> SearchAddress(GeolocationGetRequest request, CancellationToken ct);
    }
}
