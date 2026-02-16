using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService;

public interface ISocietyBusForService
{
    Task<SocietyCreateResponse> CreateSociety(SocietyCreateRequest request, CancellationToken ct);

    Task<SocietiesResponse> GetSocieties(CancellationToken ct);
}
