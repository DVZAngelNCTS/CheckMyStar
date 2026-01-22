using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAddressDal
    {
        Task<AddressResult> GetAddress(int addressIdentifier, CancellationToken ct);
    }
}
