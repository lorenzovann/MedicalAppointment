

namespace MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos
{
    public class DoctorAvailabilityBaseDto : BaseDto
    {
        public int AvailabilityID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

    }
}
