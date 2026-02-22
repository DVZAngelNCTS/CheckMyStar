using CheckMyStar.Enumerations;

namespace CheckMyStar.Bll.Requests
{
    public class UserGetRequest
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public int? SocietyIdentifier { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? Role { get; set; }
    }
}
