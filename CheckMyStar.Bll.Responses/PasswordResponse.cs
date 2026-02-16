using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class PasswordResponse : BaseResponse
    {
        public bool IsValid { get; set; }
        public required LoginModel Login { get; set; }
    }
}
