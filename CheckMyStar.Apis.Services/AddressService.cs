using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class AddressService(IAddressBusForService addressBusForService) : IAddressService
    {
        public Task<AddressResponse> GetNextIdentifier(CancellationToken ct)
        {
            return addressBusForService.GetNextIdentifier(ct);
        }

        public Task<BaseResponse> AddAddress(AddressSaveRequest request, CancellationToken ct)
        {
            return addressBusForService.AddAddress(request, ct);
        }
    }
}
