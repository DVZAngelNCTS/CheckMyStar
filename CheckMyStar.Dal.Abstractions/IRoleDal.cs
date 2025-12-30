using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IRoleDal
    {
        Task<List<Role>> GetRoles();
    }
}
