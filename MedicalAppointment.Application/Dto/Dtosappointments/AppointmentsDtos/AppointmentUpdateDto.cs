
namespace MedicalAppointment.Application.Dto.Dtosappointments.Appointments
{
    public class AppointmentUpdateDto
    {
        public int StatusID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
