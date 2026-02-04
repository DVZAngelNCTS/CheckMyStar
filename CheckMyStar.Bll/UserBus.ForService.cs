using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class UserBus : IUserBusForService
    {
        public Task<UserResponse> GetUser(LoginGetRequest request, CancellationToken ct)
        {
            return this.GetUser(request.Login, request.Password, ct);
        }

        public Task<UsersResponse> GetUsers(UserGetRequest request, CancellationToken ct)
        {
            return this.GetUsers(request.LastName, request.FirstName, request.Society, request.Email, request.Phone, request.Address, request.Role, ct);
        }

        public Task<BaseResponse> AddUser(UserSaveRequest request, CancellationToken ct)
        {
            return this.AddUser(request.User, ct);
        }

        public Task<BaseResponse> UpdateUser(UserSaveRequest request, CancellationToken ct)
        {
            return this.UpdateUser(request.User, ct);
        }

        public Task<BaseResponse> DeleteUser(UserDeleteRequest request, CancellationToken ct)
        {
            return this.DeleteUser(request.Identifier, ct);
        }
    }
}
