using AutoMapper;
using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll;

public partial class SocietyBus(ISocietyDal societyDal, IMapper mapper, IUserContextService userContext, IActivityBus activityBus) : ISocietyBus
{
    public async Task<SocietyCreateResponse> CreateSociety(SocietyModel societyModel, int currentUser, CancellationToken ct)
    {
        var response = new SocietyCreateResponse();

        try
        {
            var society = mapper.Map<Society>(societyModel);

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
            
            await activityBus.AddActivity(dalResult.Message, DateTime.Now, currentUser, dalResult.IsSuccess, ct);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Erreur inattendue : {ex.Message}";
        }

        return response;
    }

    public async Task<SocietiesResponse> GetAllSocieties(CancellationToken ct)
    {
        var response = new SocietiesResponse();

        try
        {
            var dalResult = await societyDal.GetSocieties(ct);

            if (dalResult.IsSuccess)
            {
                response.IsSuccess = true;
                response.Societies = mapper.Map<List<SocietyModel>>(dalResult.Societies);
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
            response.Message = ex.Message;
        }

        return response;
    }
}