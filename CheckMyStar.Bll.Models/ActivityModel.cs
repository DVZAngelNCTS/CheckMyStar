namespace CheckMyStar.Bll.Models
{
    public class ActivityModel
    {
        public int Identifier { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public UserModel? User { get; set; }
        public bool IsSuccess { get; set; }
    }
}
