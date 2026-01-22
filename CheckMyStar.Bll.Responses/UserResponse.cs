using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class UserResponse : BaseResponse
    {
        public bool IsValid { get; set; }
        public UserModel? User { get; set; }
    }
}
