using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll;

public partial class SocietyBus(ISocietyDal societyDal, IMapper mapper, IUserContextService userContext, IActivityBus activityBus, IAddressDal addressDal) : ISocietyBus
{
    public async Task<SocietyResponse> GetIdentifier(CancellationToken ct)
    {
        var society = await societyDal.GetNextIdentifier(ct);

        return mapper.Map<SocietyResponse>(society);
    }

    public async Task<BaseResponse> AddSociety(SocietyModel societyModel, int currentUser, CancellationToken ct)
    {
        BaseResponse response = new BaseResponse();

        var societyResult = await societyDal.GetSociety(societyModel.Identifier, ct);

        if (societyResult.IsSuccess)
        {
            if (societyResult.Society == null)
            {
                societyResult = await societyDal.GetSociety(societyModel.Name, societyModel.Email, societyModel.Phone, ct);

                if (societyResult.IsSuccess)
                {
                    if (societyResult.Society == null)
                    {
                        var dateTime = DateTime.Now;

                        var addressEntity = mapper.Map<Address>(societyModel.Address);

                        var addressResult = mapper.Map<BaseResponse>(await addressDal.AddAddress(addressEntity, ct));

                        if (addressResult.IsSuccess)
                        {
                            var society = mapper.Map<Society>(societyModel);

                            var result = await societyDal.AddSociety(society, ct);

                            if (result.IsSuccess)
                            {
                                response.IsSuccess = true;
                                response.Message = result.Message;
                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = result.Message;
                            }

                            await activityBus.AddActivity(result.Message, DateTime.Now, currentUser, result.IsSuccess, ct);
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = addressResult.Message;
                        }

                        await activityBus.AddActivity(addressResult.Message, dateTime, currentUser, addressResult.IsSuccess, ct);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "La société existe déjà";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = societyResult.Message;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "La Société existe déjà";
            }
        }
        else
        {
            response.IsSuccess = false;
            response.Message = societyResult.Message;
        }

        return response;
    }

    public async Task<BaseResponse> UpdateSociety(SocietyModel societyModel, int currentUser, CancellationToken ct)
    {
        BaseResponse result = new BaseResponse();

        var societyResult = await societyDal.GetSociety(societyModel.Identifier, ct);

        if (societyResult.IsSuccess)
        {
            if (societyResult.Society != null)
            {
                var dateTime = DateTime.Now;

                societyModel.UpdatedDate = dateTime;

                var societyEntity = mapper.Map<Society>(societyModel);

                var baseResult = await societyDal.UpdateSociety(societyEntity, ct);

                if (baseResult.IsSuccess)
                {
                    if (societyModel.Address != null)
                    {
                        societyModel.Address.UpdatedDate = dateTime;

                        var addressEntity = mapper.Map<Address>(societyModel.Address);

                        var addressResult = await addressDal.UpdateAddress(addressEntity, ct);

                        if (addressResult.IsSuccess)
                        {
                            result.IsSuccess = true;
                            result.Message = baseResult.Message + "<br>" + addressResult.Message;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = baseResult.Message + "<br>" + addressResult.Message;
                        }

                        await activityBus.AddActivity(addressResult.Message, dateTime, currentUser, addressResult.IsSuccess, ct);
                    }
                    else
                    {
                        result.IsSuccess = true;
                        result.Message = baseResult.Message;
                    }

                    await activityBus.AddActivity(baseResult.Message, dateTime, currentUser, baseResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = baseResult.Message;
                }

                await activityBus.AddActivity(baseResult.Message, dateTime, currentUser, baseResult.IsSuccess, ct);

            }
            else
            {
                result.IsSuccess = false;
                result.Message = "La société n'existe pas, impossible de la modifié";
            }
        }
        else
        {
            result.IsSuccess = false;
            result.Message = societyResult.Message;
        }

        return result;
    }

    public async Task<BaseResponse> DeleteSociety(int identifier, int currentUser, CancellationToken ct)
    {
        BaseResponse result = new BaseResponse();

        var society = await societyDal.GetSociety(identifier, ct);

        if (society.IsSuccess)
        {
            if (society.Society != null)
            {
                if (society.Society.AddressIdentifier != null)
                {
                    var addressResult = await addressDal.GetAddress(society.Society.AddressIdentifier.Value, ct);

                    if (addressResult.IsSuccess)
                    {
                        if (addressResult.Address != null)
                        {
                            var deleteAddressResult = await addressDal.DeleteAddress(addressResult.Address, ct);

                            if (deleteAddressResult.IsSuccess)
                            {
                                result.Message += "<br>" + deleteAddressResult.Message;
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Message += "<br>" + deleteAddressResult.Message;
                            }

                            await activityBus.AddActivity(deleteAddressResult.Message, DateTime.Now, currentUser, deleteAddressResult.IsSuccess, ct);
                        }
                    }
                }

                var deleteSociety = await societyDal.DeleteSociety(society.Society, ct);

                if (deleteSociety.IsSuccess)
                {
                    result.Message += "<br>" + deleteSociety.Message;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message += "<br>" + deleteSociety.Message;
                }

                await activityBus.AddActivity(deleteSociety.Message, DateTime.Now, currentUser, deleteSociety.IsSuccess, ct);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "La société n'existe pas, impossible de la supprimer";
            }
        }
        else
        {
            result.IsSuccess = false;
            result.Message = society.Message;
        }

        return result;
    }

    public async Task<SocietyResponse> GetSociety(int identifier, CancellationToken ct)
    {
        SocietyResponse response = new SocietyResponse();

        var result = await societyDal.GetSociety(identifier, ct);

        if (result.IsSuccess)
        {
            response.IsSuccess = true;
            response.Society = mapper.Map<SocietyModel>(result.Society);
        }
        else
        {
            response.IsSuccess = false;
            response.Message = result.Message;
        }

        return response;
    }

    public async Task<SocietiesResponse> GetAllSocieties(CancellationToken ct)
    {
        SocietiesResponse response = new SocietiesResponse();

        var result = await societyDal.GetSocieties(ct);

        if (result.IsSuccess)
        {
            response.IsSuccess = true;
            response.Societies = mapper.Map<List<SocietyModel>>(result.Societies);
        }
        else
        {
            response.IsSuccess = false;
            response.Message = result.Message;
        }

        return response;
    }
}