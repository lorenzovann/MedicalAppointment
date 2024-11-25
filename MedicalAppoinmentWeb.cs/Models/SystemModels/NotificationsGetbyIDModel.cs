using MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs;
using MedicalWeb.cs.Models.Base;


namespace MedicalWeb.cs.Models.SystemsModel
{
    public class GetNotificationsByIdModelALl : BaseApiResponse
    {
        public GetNotificationsDtos data { get; set; }


    }
}
