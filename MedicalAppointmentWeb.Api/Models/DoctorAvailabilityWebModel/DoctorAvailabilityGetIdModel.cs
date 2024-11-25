using MedicalAppointment.Persistance.Models.appointments.DoctorAvailabilityCRUD;
using MedicalAppointment.Persistance.Repositories.appointmentsRepositories;

namespace MedicalAppointmentWeb.Api.Models.DoctorAvailabilityWebModel
{
    public class DoctorAvailabilityGetIdModel : BaseModel
    {
        public DoctorAvailabilityModel data {  get; set; }
    }
}
