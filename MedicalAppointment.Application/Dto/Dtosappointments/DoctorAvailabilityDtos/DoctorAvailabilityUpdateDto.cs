
namespace MedicalAppointment.Application.Dto.Dtosappointments.DoctorAvailabilityDtos
{
    public class DoctorAvailabilityUpdateDto : DoctorAvailabilityBaseDto
    {
        public int AvailabilityID { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
    }

}
