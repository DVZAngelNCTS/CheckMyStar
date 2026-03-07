using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;
using System.Net.Sockets;

namespace CheckMyStar.Bll
{
    public partial class AddressBus(IUserContextService userContext, IActivityBus activityBus, IAddressDal addressDal, ICountryDal countryDal, IMapper mapper) : IAddressBus
    {
        public async Task<AddressResponse> GetIdentifier(CancellationToken ct)
        {
            var address = await addressDal.GetNextIdentifier(ct);

            return mapper.Map<AddressResponse>(address);
        }

        public async Task<AddressesResponse> GetAddresses(string? number, string? street, string? city, string? zipCode, string? region, int? countryIdentifier, CancellationToken ct)
        {
            AddressesResponse addressesResponse = new AddressesResponse();

            var addressResult = await addressDal.GetAddresses(number, street, city, zipCode, region, countryIdentifier, ct);

            if (addressResult.IsSuccess)
            {
                if (addressResult.Addresses != null)
                {
                    foreach (var address in addressResult.Addresses)
                    {
                        var addressModel = mapper.Map<AddressModel>(address);

                        var countryResponse = await countryDal.GetCountry(address.CountryIdentifier, ct);

                        addressModel.Country = mapper.Map<CountryModel>(countryResponse.Country);

                        addressesResponse.Addresses.Add(addressModel);
                    }

                    addressesResponse.IsSuccess = true;
                    addressesResponse.Message = addressResult.Message;
                }
                else
                {
                    addressesResponse.IsSuccess = false; 
                    addressesResponse.Message = "Aucune adresse trouvée avec les critères spécifiés";
                }
            }
            else
            {
                addressesResponse.IsSuccess = false;
                addressesResponse.Message = addressResult.Message;
            }

            return addressesResponse;
        }

        public async Task<BaseResponse> AddAddress(AddressModel addressModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var address = await addressDal.GetAddress(addressModel.Identifier, ct);

            if (address.IsSuccess)
            {
                if (address.Address == null)
                {
                    var dateTime = DateTime.Now;

                    var addressEntity = mapper.Map<Address>(addressModel);

                    var addressResult = await addressDal.AddAddress(addressEntity, ct);

                    if (addressResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = addressResult.Message;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = addressResult.Message;
                    }

                    await activityBus.AddActivity(addressResult.Message, dateTime, currentUser, addressResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'adresse existe déjà";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = address.Message;
            }

            return result;
        }

        public async Task<BaseResponse> UpdateAddress(AddressModel addressModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var address = await addressDal.GetAddress(addressModel.Identifier, ct);

            if (address.IsSuccess)
            {
                if (address.Address != null)
                {
                    var dateTime = DateTime.Now;

                    addressModel.CreatedDate = address.Address.CreatedDate;
                    addressModel.UpdatedDate = dateTime;

                    var addressEntity = mapper.Map<Address>(addressModel);

                    var addressResult = await addressDal.UpdateAddress(addressEntity, ct);

                    if (addressResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = addressResult.Message;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = addressResult.Message;
                    }

                    await activityBus.AddActivity(addressResult.Message, dateTime, currentUser, addressResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'adresse spécifiée n'existe pas, impossible de la modifier";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = address.Message;
            }

            return result;
        }

        public async Task<BaseResponse> DeleteAddress(int identifier, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var address = await addressDal.GetAddress(identifier, ct);

            if (address.IsSuccess)
            {
                if (address.Address != null)
                {
                    var dateTime = DateTime.Now;

                    var addressResult = await addressDal.DeleteAddress(address.Address, ct);

                    if (addressResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = addressResult.Message;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = addressResult.Message;
                    }

                    await activityBus.AddActivity(addressResult.Message, dateTime, currentUser, addressResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'adresse spécifiée n'existe pas, impossible de la modifier";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = address.Message;
            }

            return result;
        }
    }
}

