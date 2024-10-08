

using MedicalAppointment.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointment.Domain.Entities.Confi.Systems
{
    [Table("Notifications", Schema = "System")]
    public sealed class SystemNotifications 
    {  

        public int NotificationId { get; set; } 
        public int UserID {  get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }    

    }
}
                   