using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions;

public interface ISocietyService
{
    Task<SocietyResponse> AddSociety(SocietySaveRequest request, CancellationToken ct);

    Task<SocietiesResponse> GetSocieties(CancellationToken ct);
}