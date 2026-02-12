using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class CriterionUpdateRequest
    {
        public int CriterionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePoints { get; set; }
        public string TypeCode { get; set; } = string.Empty;
        public byte StarLevelId { get; set; }
    }
}