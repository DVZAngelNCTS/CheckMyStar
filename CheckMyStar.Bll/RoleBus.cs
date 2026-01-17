using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Dal.Abstractions;

namespace CheckMyStar.Bll
{
    public partial class RoleBus(IRoleDal roleDal, IMapper mapper) : IRoleBus
    {
        public async Task<List<RoleModel>> GetRoles(string? name, CancellationToken ct)
        {
            var roles = await roleDal.GetRoles(name, ct);

            return mapper.Map<List<RoleModel>>(roles);
        }
    }
}
