using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IAddressBusForService
    {
        Task<AddressResponse> GetNextIdentifier(CancellationToken ct);
        Task<BaseResponse> AddAddress(AddressSaveRequest request, CancellationToken ct);
    }
}
