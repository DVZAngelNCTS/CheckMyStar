namespace CheckMyStar.Bll.Requests
{
    public class ActivitiesGetRequest
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsSuccess { get; set; }
    }
}
