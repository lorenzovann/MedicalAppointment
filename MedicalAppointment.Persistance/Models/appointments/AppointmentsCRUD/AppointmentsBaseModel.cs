

namespace MedicalAppointment.Persistance.Models.appointments.AppointmentsCRUD
{
    public class AppointmentsBaseModel
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public int AvailabilityID { get; set; }

    }
}
