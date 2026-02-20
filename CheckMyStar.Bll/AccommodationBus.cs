using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class AccommodationBus(IUserContextService userContext, IActivityBus activityBus, IAccommodationDal accommodationDal, IAccommodationTypeDal accommodationTypeDal, IAddressDal addressDal, IMapper mapper) : IAccommodationBus
    {
        public async Task<AccommodationResponse> GetIdentifier(CancellationToken ct)
        {
            var accommodation = await accommodationDal.GetNextIdentifier(ct);

            return mapper.Map<AccommodationResponse>(accommodation);
        }

        public async Task<AccommodationsResponse> GetAccommodations(CancellationToken ct)
        {
            AccommodationsResponse accommodationsResponse = new AccommodationsResponse();

            var accommodations = await accommodationDal.GetAccommodations(ct);

            if (accommodations.IsSuccess && accommodations.Accommodations != null)
            {
                foreach (Accommodation accommodation in accommodations.Accommodations)
                {
                    var accommodationModel = mapper.Map<AccommodationModel>(accommodation);

                    var addressResponse = await addressDal.GetAddress(accommodation.AddressIdentifier, ct);

                    if (addressResponse.IsSuccess && addressResponse.Address != null)
                    {
                        accommodationModel.Address = mapper.Map<AddressModel>(addressResponse.Address);
                    }

                    var accommodationTypeResponse = await accommodationTypeDal.GetAccommodationType(accommodation.AccommodationTypeIdentifier, ct);

                    if (accommodationTypeResponse.IsSuccess && accommodationTypeResponse.AccommodationType != null)
                    {
                        accommodationModel.AccommodationType = mapper.Map<AccommodationTypeModel>(accommodationTypeResponse.AccommodationType);
                    }

                    accommodationsResponse.Accommodations.Add(accommodationModel);
                }

                accommodationsResponse.IsSuccess = true;
                accommodationsResponse.Message = accommodations.Message;
            }
            else
            {
                accommodationsResponse.IsSuccess = false;
                accommodationsResponse.Message = accommodations.Message;
            }

            return accommodationsResponse;
        }

        public async Task<BaseResponse> AddAccommodation(AccommodationModel accommodationModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var accommodation = await accommodationDal.GetAccommodation(accommodationModel.Identifier, ct);

            if (accommodation.IsSuccess)
            {
                if (accommodation.Accommodation == null)
                {
                    var addressResponse = await addressDal.GetAddress(accommodationModel.Address.Identifier, ct);

                    if (!addressResponse.IsSuccess || addressResponse.Address == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "L'adresse spécifiée n'existe pas";
                        return result;
                    }

                    var accommodationTypeResponse = await accommodationTypeDal.GetAccommodationType(accommodationModel.AccommodationType.Identifier, ct);

                    if (!accommodationTypeResponse.IsSuccess || accommodationTypeResponse.AccommodationType == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "Le type d'hébergement spécifié n'existe pas";
                        return result;
                    }

                    var dateTime = DateTime.Now;

                    accommodationModel.CreatedDate = dateTime;
                    accommodationModel.UpdatedDate = dateTime;
                    accommodationModel.IsActive = true;

                    var accommodationEntity = mapper.Map<Accommodation>(accommodationModel);

                    var accommodationResult = await accommodationDal.AddAccommodation(accommodationEntity, ct);

                    if (accommodationResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = accommodationResult.Message;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = accommodationResult.Message;
                    }

                    await activityBus.AddActivity(accommodationResult.Message, dateTime, currentUser, accommodationResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'hébergement existe déjà";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = accommodation.Message;
            }

            return result;
        }

        public async Task<BaseResponse> UpdateAccommodation(AccommodationModel accommodationModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var accommodation = await accommodationDal.GetAccommodation(accommodationModel.Identifier, ct);

            if (accommodation.IsSuccess)
            {
                if (accommodation.Accommodation != null)
                {
                    var addressResponse = await addressDal.GetAddress(accommodationModel.Address.Identifier, ct);

                    if (!addressResponse.IsSuccess || addressResponse.Address == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "L'adresse spécifiée n'existe pas";
                        return result;
                    }

                    var accommodationTypeResponse = await accommodationTypeDal.GetAccommodationType(accommodationModel.AccommodationType.Identifier, ct);

                    if (!accommodationTypeResponse.IsSuccess || accommodationTypeResponse.AccommodationType == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "Le type d'hébergement spécifié n'existe pas";
                        return result;
                    }

                    var dateTime = DateTime.Now;

                    accommodationModel.UpdatedDate = dateTime;

                    var accommodationEntity = mapper.Map<Accommodation>(accommodationModel);

                    var accommodationResult = await accommodationDal.UpdateAccommodation(accommodationEntity, ct);

                    if (accommodationResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = accommodationResult.Message;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = accommodationResult.Message;
                    }

                    await activityBus.AddActivity(accommodationResult.Message, dateTime, currentUser, accommodationResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'hébergement n'existe pas, impossible de le modifier";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = accommodation.Message;
            }

            return result;
        }

        public async Task<BaseResponse> DeleteAccommodation(int accommodationIdentifier, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var accommodation = await accommodationDal.GetAccommodation(accommodationIdentifier, ct);

            if (accommodation.IsSuccess)
            {
                if (accommodation.Accommodation != null)
                {
                    var accommodationEntity = accommodation.Accommodation;

                    var accommodationResult = await accommodationDal.DeleteAccommodation(accommodationEntity, ct);

                    if (accommodationResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = accommodationResult.Message;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = accommodationResult.Message;
                    }

                    await activityBus.AddActivity(accommodationResult.Message, DateTime.Now, currentUser, accommodationResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'hébergement n'existe pas, impossible de le supprimer";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = accommodation.Message;
            }

            return result;
        }
    }
}
