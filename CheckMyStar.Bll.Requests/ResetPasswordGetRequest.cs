namespace CheckMyStar.Bll.Requests
{
    public class ResetPasswordGetRequest
    {
        public string? Token { get; set; }
        public string? NewPassword { get; set; }
    }
}
