

using MedicalAppointment.Persistance.Models.appointments.AppointmentsCRUD;

namespace MedicalAppointmentWeb.Api.Models.AppointmentsWebModel
{
    public class AppointmentsGetAllModel : BaseModel
    {
        public List<AppointmentsModel> data { get; set; }
        
    }
}
