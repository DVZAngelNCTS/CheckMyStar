namespace CheckMyStar.Bll.Models
{
    public class FolderModel
    {
        public int Identifier { get; set; }
        public AccommodationModel? Accommodation { get; set; }
        public UserModel? Owner { get; set; }
        public UserModel? Inspector { get; set; }
        public FolderStatusModel? FolderStatus { get; set; }
        public QuoteModel? Quote { get; set; }
        public InvoiceModel? Invoice { get; set; }
        public AppointmentModel? Appointment { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
