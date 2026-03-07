using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class SocietySaveRequest
    {
        public required SocietyModel Society { get; set; }
    }
}
