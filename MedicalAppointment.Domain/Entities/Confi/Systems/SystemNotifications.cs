

using MedicalAppointment.Domain.Base;

namespace MedicalAppointment.Domain.Entities.Confi.Systems
{
    public sealed class SystemNotifications 
    {  

        public int NotificationId { get; set; } 
        public int UserID {  get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }    

    }
}
