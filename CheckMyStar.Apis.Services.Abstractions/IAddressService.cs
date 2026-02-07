using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IAddressService
    {
        Task<AddressResponse> GetNextIdentifier(CancellationToken ct);
    }
}
