using Azure.Core;
using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services;

public class SocietyService(ISocietyBusForService societyBusForService) : ISocietyService
{
    public async Task<SocietyResponse> GetNextIdentifier(CancellationToken ct)
    {
        var society = await societyBusForService.GetNextIdentifier(ct);

        return society;
    }

    public async Task<BaseResponse> AddSociety(SocietySaveRequest request, CancellationToken ct)
    {
        var response = await societyBusForService.AddSociety(request, ct);
        
        return response;
    }

    public async Task<BaseResponse> UpdateSociety(SocietySaveRequest request, CancellationToken ct)
    {
        var result = await societyBusForService.UpdateSociety(request, ct);

        return result;
    }

    public async Task<BaseResponse> DeleteSociety(SocietyDeleteRequest request, CancellationToken ct)
    {
        var result = await societyBusForService.DeleteSociety(request, ct);

        return result;
    }

    public async Task<BaseResponse> EnabledSociety(SocietySaveRequest request, CancellationToken ct)
    {
        var result = await societyBusForService.EnabledSociety(request, ct);

        return result;
    }

    public async Task<SocietiesResponse> GetSocieties(SocietyGetRequest request, CancellationToken ct)
    {
        var response = await societyBusForService.GetSocieties(request, ct);

        return response;
    }

    public async Task<SocietyResponse> GetSociety(SocietyGetRequest request, CancellationToken ct)
    {
        var response = await societyBusForService.GetSociety(request, ct);

        return response;
    }
}