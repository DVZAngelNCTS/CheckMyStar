namespace CheckMyStar.Bll.Models
{
    public class FolderModel
    {
        public int Identifier { get; set; }
        public AccommodationTypeModel AccommodationType { get; set; } = null!;
        public AccommodationModel Accommodation { get; set; } = null!;
        public UserModel OwnerUser { get; set; } = null!;
        public UserModel InspectorUser { get; set; } = null!;
        public FolderStatusModel FolderStatus { get; set; } = null!;
        public QuoteModel? Quote { get; set; }
        public InvoiceModel? Invoice { get; set; }
        public AppointmentModel? Appointment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
