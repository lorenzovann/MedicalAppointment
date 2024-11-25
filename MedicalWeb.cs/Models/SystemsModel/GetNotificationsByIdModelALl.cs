using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalWeb.cs.Models.BaseModel;

namespace MedicalWeb.cs.Models.SystemsModel
{
    public class GetNotificationsByIdModelALl : BaseApiResponse
    { 
        public GetNotificationsDtos data { get; set; } 


    }
}
