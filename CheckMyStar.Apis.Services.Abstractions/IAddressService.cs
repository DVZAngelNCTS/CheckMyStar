using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IAddressService
    {
        Task<AddressResponse> GetNextIdentifier(CancellationToken ct);
        Task<AddressesResponse> GetAddresses(AddressGetRequest request, CancellationToken ct);
        Task<BaseResponse> AddAddress(AddressSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateAddress(AddressSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteAddress(AddressDeleteRequest request, CancellationToken ct);
        Task<GeolocationResponse> SearchAddress(GeolocationGetRequest request, CancellationToken ct);
    }
}
