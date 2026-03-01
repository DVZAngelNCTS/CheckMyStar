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
    public async Task<SocietyResponse> AddSociety(SocietyModel societyModel, int currentUser, CancellationToken ct)
    {
        var response = new SocietyResponse();

        try
        {
            var society = mapper.Map<Society>(societyModel);

            var result = await societyDal.AddSociety(society, ct);

            if (result.IsSuccess)
            {
                societyModel = mapper.Map<SocietyModel>(result.Society);

                response.IsSuccess = true;
                response.Message = result.Message;
                response.Society = societyModel;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = result.Message;
            }
            
            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);
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