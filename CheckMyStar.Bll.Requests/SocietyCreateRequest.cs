using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMyStar.Bll.Requests
{
    public class SocietyCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? AddressIdentifier { get; set; }
    }
}
