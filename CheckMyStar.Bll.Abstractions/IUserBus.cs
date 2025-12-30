using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IUserBus
    {
        public Task<UserModel?> GetUser(string login, string password);
    }
}
