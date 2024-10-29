

namespace MedicalAppoiment.Aplication.cs.Dtos.Systems.NotificationsDtos.cs
{
    public abstract class BaseNotificationsDto
    {
      
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
