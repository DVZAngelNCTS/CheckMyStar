using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IUserBus
    {
        Task<UserModel?> GetUser(string login, string password);
        Task<List<UserModel>> GetUsers();
    }
}
