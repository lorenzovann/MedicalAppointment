

namespace MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs
{
    public class GetNotificationsDtos
    {
        public int NotificationId { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }    
        public DateTime? SentAt { get; set; }
    }
}
