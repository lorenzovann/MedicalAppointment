

using MedicalAppoiment.Infraectructure.Core;
using MedicalAppoiment.Infraectructure.Interfaces;
using MedicalAppoiment.Infraectructure.Model;
using System.Net.Mail;

namespace MedicalAppoiment.Infraectructure.Services
{
    public class SendsmsServices : SendNotifiactionsBySMS
    {
        private readonly SmtpClient _cliente;


        public SendsmsServices(SmtpClient cliente) {

            this._cliente = cliente;
        } 

            
        public Task<NotificationsResult> SendSmS(SmsModel sendSMS)
        {
            throw new NotImplementedException();
        }
    }
}
