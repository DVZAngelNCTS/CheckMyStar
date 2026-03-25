namespace CheckMyStar.Bll.Models
{
    public class LoginModel
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string? ResetToken { get; set;  }
        public DateTime? ResetTokenExpiration { get; set; }
        public UserModel? User { get; set; }
    }
}
