using System;
using System.Collections.Generic;

namespace CheckMyStar.Bll.Models
{
    public class StarStatusDto
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class StarCriteriaDto
    {
        public int Rating { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime LastUpdate { get; set; }
        public List<StarStatusDto> Statuses { get; set; } = new();
    }
}
