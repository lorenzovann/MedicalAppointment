using MedicalAppointment.Persistance.Models.appointments.DoctorAvailabilityCRUD;

namespace MedicalAppointmentWeb.Api.Models.DoctorAvailabilityWebModel
{
    public class DoctorAvailabilityGetAllModel : BaseModel
    {
        public List<DoctorAvailabilityModel> data { get; set; }
    }
}
