namespace CheckMyStar.Bll.Models
{
    public class AccommodationModel
    {
        public int Identifier { get; set; }
        public string? AccommodationName { get; set; }
        public string? AccommodationPhone { get; set; }
        public AccommodationTypeModel? AccommodationType { get; set; }
        public int? AccommodationCurrentStar { get; set; }
        public AddressModel? Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
