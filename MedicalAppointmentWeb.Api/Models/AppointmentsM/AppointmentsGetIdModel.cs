using MedicalAppointment.Domain.Entities.appointments;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicalAppointmentWeb.Api.Models.AppointmentsM
{
    public class AppointmentsGetIdModel : BaseModel
    {
        public Appointments data { get; set; }
       
    }
}
