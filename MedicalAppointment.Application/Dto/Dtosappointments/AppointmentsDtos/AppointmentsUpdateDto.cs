
namespace MedicalAppointment.Application.Dto.Dtosappointments.Appointments
{
    public class AppointmentsUpdateDto  : AppointmentsBaseDto
    {
        public int AppointmentID { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
