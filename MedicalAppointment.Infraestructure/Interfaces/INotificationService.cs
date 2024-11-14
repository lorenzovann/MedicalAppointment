
using MedicalAppointment.Infraestructure.Models;
using MedicalAppointment.Infraestructure.Results;

namespace MedicalAppointment.Infraestructure.Interfaces
{
    public interface INotificacionService
    {
        Task<NotificationResult> SendEmailAsync(EmailModel emailModel);
        Task<NotificationResult> SendSmsAsync(SmsModel smsModel);
        Task<NotificationResult> SendPushNotification(PushNotificationModel pushModel);
    }
}
