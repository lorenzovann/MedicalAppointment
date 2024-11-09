
namespace MedicalAppointment.Application.Dto.Dtosappointments.Appointments
{
    public class AppointmentUpdateDto
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
        public bool? IsActive { get; set; }
    }
}
