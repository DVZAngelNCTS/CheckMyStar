using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IUserDal
    {
        Task<User?> GetUser(string login, string password);
        Task<List<User>> GetUsers();
    }
}
