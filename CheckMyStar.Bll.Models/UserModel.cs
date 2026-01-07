using CheckMyStar.Enumerations;

namespace CheckMyStar.Bll.Models
{
    public class UserModel
    {
        public int Identifier { get; set; }
        public EnumCivility Civility { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Society { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public EnumRole Role { get; set; }
        public AddressModel Address { get; set; } = null!;
    }
}
