
namespace MedicalAppointment.Persistance.Model.Systems
{
    public class NotificationsModel
    {
        public int NotificationId { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
