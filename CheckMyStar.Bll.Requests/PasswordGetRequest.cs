namespace CheckMyStar.Bll.Requests
{
    public class PasswordGetRequest
    {
        public string Login { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

    }
}
