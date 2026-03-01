using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services;

public class SocietyService(ISocietyBusForService societyBus) : ISocietyService
{
    public async Task<SocietyResponse> AddSociety(SocietySaveRequest request, CancellationToken ct)
    {
        var response = await societyBus.AddSociety(request, ct);
        
        return response;
    }

    public async Task<SocietiesResponse> GetSocieties(CancellationToken ct)
    {
        var response = await societyBus.GetSocieties(ct);

        return response;
    }
}