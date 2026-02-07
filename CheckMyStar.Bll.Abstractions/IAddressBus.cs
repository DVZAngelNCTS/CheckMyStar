using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IAddressBus
    {
        Task<AddressResponse> GetIdentifier(CancellationToken ct);
    }
}
