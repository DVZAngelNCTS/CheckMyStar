using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IGeolocationService
    {
        Task<GeolocationResponse> Search(string address, CancellationToken ct);
    }
}
