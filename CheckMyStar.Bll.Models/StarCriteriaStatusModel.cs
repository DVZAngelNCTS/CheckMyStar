namespace CheckMyStar.Bll.Models
{
    public class StarCriteriaStatusModel
    {
        public int Rating { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<StarStatusModel>? Statuses { get; set; }
    }
}
