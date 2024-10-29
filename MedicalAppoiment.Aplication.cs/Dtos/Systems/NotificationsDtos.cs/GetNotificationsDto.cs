
namespace MedicalAppoiment.Aplication.cs.Dtos.Systems.NotificationsDtos.cs
{
    public class GetNotificationsDto
    {
        public int NotificationId { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
