using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService;

public interface ISocietyBusForService
{
    Task<SocietyCreateResponse> CreateSociety(SocietySaveRequest request, CancellationToken ct);
    Task<SocietiesResponse> GetSocieties(CancellationToken ct);
}
