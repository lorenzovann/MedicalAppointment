using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalWeb.cs.Models.Base;
using MedicalWeb.cs.Models.BaseModel;

namespace MedicalWeb.cs.Models.SystemsModel
{
    public class NotificationsGetAllModel : BaseApiResponse
    {
        public List<GetNotificationsDtos> data { get; set; }


    }
}