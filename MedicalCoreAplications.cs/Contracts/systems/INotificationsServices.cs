using Medical.Domain.Entities.Confi.Systems;
using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCoreAplications.cs.Contracts.Systems
{
    public interface INotificationServices : IBaseServices<NotificationsResponse, SaveNotificationsDto, UpdateNotificationsDtos, GetNotificationsDtos>
    {
   
    }
}
