namespace CheckMyStar.Bll.Models
{
    public class StarCriteriaModel
    {
        public int Rating { get; set; }
        public string? StarLabel { get; set; }
        public List<StarCriteriaDetailModel>? Criteria { get; set; }
    }
}
