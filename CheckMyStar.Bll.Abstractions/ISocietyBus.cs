using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface ISocietyBus
    {
        Task<SocietyCreateResponse> CreateSociety(SocietyCreateRequest request, CancellationToken ct);

        Task<SocietiesResponse> GetSocieties(CancellationToken ct);
    }
}
