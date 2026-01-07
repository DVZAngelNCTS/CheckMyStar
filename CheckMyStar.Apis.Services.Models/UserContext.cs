using CheckMyStar.Enumerations;

namespace CheckMyStar.Apis.Services.Models
{
    public class UserContext
    {
        public int Identifier { get; set; } = 0;
        public string Token { get; set; } = string.Empty;
        public EnumCivility Civility { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Society { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public EnumRole Role { get; set; }
        public string Number { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string? Region { get; set; }
        public string Country { get; set; } = null!;
    }
}
