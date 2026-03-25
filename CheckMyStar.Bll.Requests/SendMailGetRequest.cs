namespace CheckMyStar.Bll.Requests
{
    public class SendMailGetRequest
    {
        public string? To { get; set; }
        public string? ResetLink { get; set; }
    }
}
