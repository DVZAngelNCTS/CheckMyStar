using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services;

public class SocietyService(ISocietyBusForService societyBus) : ISocietyService
{
    public async Task<SocietyResponse> GetNextIdentifier(CancellationToken ct)
    {
        var society = await societyBus.GetNextIdentifier(ct);

        return society;
    }

    public async Task<BaseResponse> AddSociety(SocietySaveRequest request, CancellationToken ct)
    {
        var response = await societyBus.AddSociety(request, ct);
        
        return response;
    }

    public async Task<BaseResponse> UpdateSociety(SocietySaveRequest request, CancellationToken ct)
    {
        var result = await societyBus.UpdateSociety(request, ct);

        return result;
    }

    public async Task<BaseResponse> DeleteSociety(SocietyDeleteRequest request, CancellationToken ct)
    {
        var result = await societyBus.DeleteSociety(request, ct);

        return result;
    }

    public async Task<SocietiesResponse> GetSocieties(CancellationToken ct)
    {
        var response = await societyBus.GetSocieties(ct);

        return response;
    }

    public async Task<SocietyResponse> GetSociety(SocietyGetRequest request, CancellationToken ct)
    {
        var response = await societyBus.GetSociety(request, ct);

        return response;
    }
}