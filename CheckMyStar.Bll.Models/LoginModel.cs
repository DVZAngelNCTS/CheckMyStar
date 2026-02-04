namespace CheckMyStar.Bll.Models
{
    public class LoginModel
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public UserModel? User { get; set; }
    }
}
