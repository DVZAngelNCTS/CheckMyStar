using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class UserSaveRequest
    {
        public required UserModel User { get; set; }
    }
}
