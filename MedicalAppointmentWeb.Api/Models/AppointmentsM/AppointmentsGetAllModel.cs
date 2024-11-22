


using MedicalAppointment.Domain.Entities.appointments;
using MedicalAppointment.Persistance.Models.appointments.AppointmentsCRUD;

namespace MedicalAppointmentWeb.Api.Models.AppointmentsM
{
    public class AppointmentsGetAllModel : BaseModel
    {
        public List<Appointments> data { get; set; }
        
    }
}
