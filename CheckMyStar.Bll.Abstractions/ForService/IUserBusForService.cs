using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IUserBusForService
    {
        Task<UserModel?> GetUser(LoginGetRequest request);
        Task<List<UserModel>> GetUsersForService();
    }
}
