

using MedicalAppointment.Domain.Base;

namespace MedicalAppointment.Domain.Entities.Configuration
{
    public class Appointments : BaseEntity
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
       
    }
}
