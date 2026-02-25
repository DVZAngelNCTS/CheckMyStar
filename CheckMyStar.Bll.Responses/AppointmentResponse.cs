using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class AppointmentResponse : BaseResponse
    {
        public AppointmentModel? Appointment { get; set; }
    }
}
