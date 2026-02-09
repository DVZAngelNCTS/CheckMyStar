using CheckMyStar.Data;

namespace CheckMyStar.Dal.Models
{
    public class UserActivity
    {
        public int Identifier { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public User? User { get; set; }
        public bool IsSuccess { get; set; }
    }
}
