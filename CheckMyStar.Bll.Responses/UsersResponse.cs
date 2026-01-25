using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class UsersResponse : BaseResponse
    {
        public bool IsValid { get; set; }
        public List<UserModel>? Users{ get; set; }
    }
}
