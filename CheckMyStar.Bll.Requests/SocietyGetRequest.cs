namespace CheckMyStar.Bll.Requests
{
    public class SocietyGetRequest
    {
        public int? Identifier { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
