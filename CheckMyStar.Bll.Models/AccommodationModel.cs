namespace CheckMyStar.Bll.Models
{
    public class AccommodationModel
    {
        public int Identifier { get; set; }
        public string AccommodationName { get; set; } = null!;
        public string? AccommodationPhone { get; set; }
        public AccommodationTypeModel AccommodationType { get; set; } = null!;
        public int? AccommodationCurrentStar { get; set; }
        public AddressModel Address { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
