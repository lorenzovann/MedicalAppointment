

using MedicalAppoiment.Infraectructure.Core;
using MedicalAppoiment.Infraectructure.Model;

namespace MedicalAppoiment.Infraectructure.Interfaces
{
    public interface ISendNotificationsbyEmail
    {

        Task<NotificationsResult> SendEmail(EmailModel Email); 

    }
}
