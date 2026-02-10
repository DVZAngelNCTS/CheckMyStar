namespace CheckMyStar.Dal.Models
{
    public class StarCriteriaRawRow
    {
        public int Rating { get; set; }
        public string StarLabel { get; set; } = string.Empty;
        public string TypeCode { get; set; } = string.Empty;
        public string TypeLabel { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
