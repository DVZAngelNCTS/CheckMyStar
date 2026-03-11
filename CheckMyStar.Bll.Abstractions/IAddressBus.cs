using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IAddressBus
    {
        Task<AddressResponse> GetIdentifier(CancellationToken ct);
        Task<AddressesResponse> GetAddresses(string? number, string? street, string? city, string? zipCode, string? region, int? countryIdentifier, CancellationToken ct);
        Task<BaseResponse> AddAddress(AddressModel addressModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> UpdateAddress(AddressModel addressModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteAddress(int identifier, int currentUser, CancellationToken ct);
    }
}
