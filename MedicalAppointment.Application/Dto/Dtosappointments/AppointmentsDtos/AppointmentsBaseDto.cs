namespace MedicalAppointment.Application.Dto.Dtosappointments.Appointments
{
    public class AppointmentsBaseDto : BaseDto
    {

        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
       




    }
}
