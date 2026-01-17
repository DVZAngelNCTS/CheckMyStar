using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IAddressDal
    {
        Task<Address?> GetAddress(int addressIdentifier, CancellationToken ct);
    }
}
