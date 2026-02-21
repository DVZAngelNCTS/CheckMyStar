namespace CheckMyStar.Bll.Models
{
    public class StarLevelModel
    {
        public byte StarLevelId { get; set; }
        public string Label { get; set; } = null!;
        public DateTime? LastUpdate { get; set; }
    }
}
