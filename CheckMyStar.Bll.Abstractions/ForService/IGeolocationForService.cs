using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IGeolocationForService
    {
        Task<GeolocationResponse> SearchAddress(GeolocationGetRequest request, CancellationToken ct);
    }
}
