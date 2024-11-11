

using MedicalAppoiment.Infraectructure.Core;
using MedicalAppoiment.Infraectructure.Model;

namespace MedicalAppoiment.Infraectructure.Interfaces
{
    public interface SendNotifiactionsBySMS
    {
        Task<NotificationsResult> SendSmS(SmsModel sendSMS);
    }
}
