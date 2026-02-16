using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions;

public interface ISocietyService
{
    Task<SocietyCreateResponse> CreateSociety(SocietyCreateRequest request, CancellationToken ct);

    Task<SocietiesResponse> GetSocieties(CancellationToken ct);
}