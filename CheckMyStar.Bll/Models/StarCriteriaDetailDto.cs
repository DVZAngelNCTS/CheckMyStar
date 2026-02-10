using System.Collections.Generic;

namespace CheckMyStar.Bll.Models
{
    public class StarCriteriaDetailDto
    {
        public int Rating { get; set; }
        public string StarLabel { get; set; } = string.Empty;
        public List<StarCriterionDto> Criteria { get; set; } = new();
    }

    public class StarCriterionDto
    {
        public int CriterionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal BasePoints { get; set; }
        public string TypeCode { get; set; } = string.Empty;
        public string TypeLabel { get; set; } = string.Empty;
    }
}
