using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class AddressService(IAddressBusForService addressBusForService, IGeolocationBusForService geolocationBusForService) : IAddressService
    {
        public Task<AddressResponse> GetNextIdentifier(CancellationToken ct)
        {
            return addressBusForService.GetNextIdentifier(ct);
        }

        public Task<AddressesResponse> GetAddresses(AddressGetRequest request, CancellationToken ct)
        {
            return addressBusForService.GetAddresses(request, ct);
        }

        public Task<BaseResponse> AddAddress(AddressSaveRequest request, CancellationToken ct)
        {
            return addressBusForService.AddAddress(request, ct);
        }

        public Task<BaseResponse> UpdateAddress(AddressSaveRequest request, CancellationToken ct)
        {
            return addressBusForService.UpdateAddress(request, ct);
        }

        public Task<BaseResponse> DeleteAddress(AddressDeleteRequest request, CancellationToken ct)
        {
            return addressBusForService.DeleteAddress(request, ct);
        }
        public Task<GeolocationResponse> SearchAddress(GeolocationGetRequest request, CancellationToken ct)
        {
            return geolocationBusForService.SearchAddress(request, ct);
        }
    }
}
