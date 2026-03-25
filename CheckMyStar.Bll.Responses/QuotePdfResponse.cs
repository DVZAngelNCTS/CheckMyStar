namespace CheckMyStar.Bll.Responses
{
    public class QuotePdfResponse : BaseResponse
    {
        public byte[]? Content { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
