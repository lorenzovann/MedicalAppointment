
namespace MedicalAppointment.Application.Dto
{
    public class BaseDto
    {
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool IsActive { get; set; }
    }
}
