using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IRoleBus
    {
        Task<List<RoleModel>> GetRoles(string? name, CancellationToken ct);
    }
}
