using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;

namespace CheckMyStar.Bll
{
    public partial class AddressBus(IAddressDal addressDal, IMapper mapper) : IAddressBus
    {
        public async Task<AddressResponse> GetIdentifier(CancellationToken ct)
        {
            var address = await addressDal.GetNextIdentifier(ct);

            return mapper.Map<AddressResponse>(address);
        }
    }
}
