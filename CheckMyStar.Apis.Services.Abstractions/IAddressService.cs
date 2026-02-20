using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IAddressService
    {
        Task<AddressResponse> GetNextIdentifier(CancellationToken ct);
        Task<BaseResponse> AddAddress(AddressSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateAddress(AddressSaveRequest request, CancellationToken ct);
    }
}
