using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class LoginResponse : BaseResponse
    {
        public bool IsValid { get; set; }
        public required LoginModel Login { get; set; }
    }
}
