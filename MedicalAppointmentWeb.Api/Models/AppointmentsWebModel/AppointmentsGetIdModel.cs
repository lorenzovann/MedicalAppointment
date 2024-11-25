using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Persistance.Models.appointments.AppointmentsCRUD;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicalAppointmentWeb.Api.Models.AppointmentsWebModel
{
    public class AppointmentsGetIdModel : BaseModel
    {
        public AppointmentsModel data { get; set; }
       
    }
}
