
namespace MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos
{
    public class DoctorAvailabilitySaveDto
    {
        public int DoctorID { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}
