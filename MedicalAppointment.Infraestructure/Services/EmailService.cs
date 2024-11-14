

using MedicalAppointment.Infraestructure.Interfaces;
using MedicalAppointment.Infraestructure.Models;
using MedicalAppointment.Infraestructure.Results;
using System.Net.Mail;
using System.Net;

namespace MedicalAppointment.Infraestructure.Services
{
    public class EmailService : INotificacionService
    {

        public async Task<NotificationResult> SendEmailAsync(EmailModel emailModel)
        {
            NotificationResult result = new NotificationResult();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Host = "";
                    client.Port = 0;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("user", "pwd");

                    var message = new MailMessage(emailModel.From!, emailModel.To!);
                    message.Body = emailModel.Body;
                    message.IsBodyHtml = true;
                    message.Subject = emailModel.Subject;

                    await client.SendMailAsync(message);

                }
            }
            catch (Exception ex)
            {

                result.Message = $"Error realizando la notificación {ex.Message}";
            }

            return result;
        }

        public Task<NotificationResult> SendPushNotification(PushNotificationModel pushModel)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationResult> SendSmsAsync(SmsModel smsModel)
        {
            throw new NotImplementedException();
        }
    }
}

