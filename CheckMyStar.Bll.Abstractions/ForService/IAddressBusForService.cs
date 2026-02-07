using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IAddressBusForService
    {
        Task<AddressResponse> GetNextIdentifier(CancellationToken ct);
    }
}
