

using MedicalAppoiment.Aplication.cs.Base;
using MedicalAppoiment.Aplication.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalAppoiment.Aplication.cs.Response.systems;

namespace MedicalAppoiment.Aplication.cs.Contracts.systems
{
    public interface INotificationsServices : IBaseServices<NotificationsResponses, NotificationsSaveDto, NotifiactionsUpdate>
    { 

    }
}
