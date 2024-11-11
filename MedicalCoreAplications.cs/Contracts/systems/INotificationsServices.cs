

using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;

namespace MedicalCoreAplications.cs.Contracts.systems
{
    public interface INotificationsServices : IBaseServices<NotificationsResponse, SaveNotifications, UpdateNotifications>
    {
    }
}
