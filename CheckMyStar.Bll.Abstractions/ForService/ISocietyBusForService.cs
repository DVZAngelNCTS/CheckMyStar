using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService;

public interface ISocietyBusForService
{
    Task<SocietyResponse> AddSociety(SocietySaveRequest request, CancellationToken ct);
    Task<SocietiesResponse> GetSocieties(CancellationToken ct);
}
