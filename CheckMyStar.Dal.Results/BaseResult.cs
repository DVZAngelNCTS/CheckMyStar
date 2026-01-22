namespace CheckMyStar.Dal.Results
{
    public class BaseResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
