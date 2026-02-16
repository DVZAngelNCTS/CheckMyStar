using AutoMapper;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Bll;

public class SocietyBus(ISocietyDal societyDal, IMapper mapper) : ISocietyBus, ISocietyBusForService
{
    public async Task<SocietyCreateResponse> CreateSociety(SocietyCreateRequest request, CancellationToken ct)
    {
        var response = new SocietyCreateResponse();
        try
        {
            var society = mapper.Map<Society>(request);

            var dalResult = await societyDal.AddSociety(society, ct);

            if (dalResult.IsSuccess)
            {
                response.IsSuccess = true;
                response.Message = dalResult.Message;
                response.SocietyId = dalResult.SocietyId;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = dalResult.Message;
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Erreur inattendue : {ex.Message}";
        }
        return response;
    }
}