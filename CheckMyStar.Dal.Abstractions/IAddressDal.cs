using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAddressDal
    {
        Task<AddressResult> GetNextIdentifier(CancellationToken ct);
        Task<AddressResult> GetAddress(int addressIdentifier, CancellationToken ct);
        Task<BaseResult> AddAddress(Address address, CancellationToken ct);
        Task<BaseResult> UpdateAddress(Address address, CancellationToken ct);
        Task<BaseResult> DeleteAddress(Address address, CancellationToken ct);
    }
}
