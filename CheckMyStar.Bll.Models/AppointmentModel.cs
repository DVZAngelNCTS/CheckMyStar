namespace CheckMyStar.Bll.Models
{
    public class AppointmentModel
    {
        public int Identifier { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AddressModel? Address { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}