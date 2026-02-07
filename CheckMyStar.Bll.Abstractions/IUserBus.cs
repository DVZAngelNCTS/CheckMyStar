using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IUserBus
    {
        Task<UserResponse> GetIdentifier(CancellationToken ct);
        Task<UserResponse> GetUser(string login, string password, CancellationToken ct);
        Task<UsersResponse> GetUsers(string lastName, string firstName, string society, string email, string phone, string address, int? role, CancellationToken ct);
        Task<BaseResponse> AddUser(UserModel userModel, CancellationToken ct);
        Task<BaseResponse> UpdateUser(UserModel userModel, CancellationToken ct);
        Task<BaseResponse> DeleteUser(int identifier, CancellationToken ct);
    }
}
