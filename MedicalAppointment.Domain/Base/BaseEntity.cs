

namespace MedicalAppointment.Domain.Base
{
    public abstract class BaseEntity
    {
        public DateTime CreateAt { get; set; } 
        public DateTime UpdateAt { get; set; }
        public bool IsActive { get; set; } 
        public string PhoneNumber { get; set; } 

    }
}
