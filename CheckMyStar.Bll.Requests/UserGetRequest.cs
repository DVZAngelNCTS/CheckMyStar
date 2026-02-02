using CheckMyStar.Enumerations;

namespace CheckMyStar.Bll.Requests
{
    public class UserGetRequest
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Society { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int? Role { get; set; }
    }
}
