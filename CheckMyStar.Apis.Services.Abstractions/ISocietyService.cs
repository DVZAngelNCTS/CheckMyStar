using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions;

public interface ISocietyService
{
    Task<SocietyResponse> GetNextIdentifier(CancellationToken ct);
    Task<BaseResponse> AddSociety(SocietySaveRequest request, CancellationToken ct);
    Task<BaseResponse> UpdateSociety(SocietySaveRequest request, CancellationToken ct);
    Task<BaseResponse> DeleteSociety(SocietyDeleteRequest request, CancellationToken ct);
    Task<BaseResponse> EnabledSociety(SocietySaveRequest request, CancellationToken ct);
    Task<SocietiesResponse> GetSocieties(SocietyGetRequest request, CancellationToken ct);
    Task<SocietyResponse> GetSociety(SocietyGetRequest request, CancellationToken ct);
}