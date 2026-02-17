using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IAddressBus
    {
        Task<AddressResponse> GetIdentifier(CancellationToken ct);
        Task<BaseResponse> AddAddress(AddressModel addressModel, int currentUser, CancellationToken ct);
    }
}
