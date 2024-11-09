

namespace MedicalAppointment.Application.Dto.Dtosappointments.Appointments
{
    public class AppointmentSaveDto
    {

        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }

    }
}
