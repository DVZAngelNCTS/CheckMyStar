using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class AppointmentSaveRequest
    {
        public required AppointmentModel Appointment { get; set; }
        public int FolderIdentifier { get; set; }
    }
}
