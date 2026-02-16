using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services;

public class SocietyService(ISocietyBusForService societyBus) : ISocietyService
{
    public async Task<SocietyCreateResponse> CreateSociety(SocietyCreateRequest request, CancellationToken ct)
    {
        return await societyBus.CreateSociety(request, ct);
    }
}