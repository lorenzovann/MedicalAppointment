using MedicalAppoiment.Infraectructure.Core;
using MedicalAppoiment.Infraectructure.Interfaces;
using MedicalAppoiment.Infraectructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppoiment.Infraectructure.Services
{
    public class SendEmailServices : ISendNotificationsbyEmail
    {     

        private readonly SmtpClient smtpClient;

        public SendEmailServices(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
        } 

        public Task<NotificationsResult> SendEmail(EmailModel Email)
        {
            throw new NotImplementedException();
        }
    }
}
