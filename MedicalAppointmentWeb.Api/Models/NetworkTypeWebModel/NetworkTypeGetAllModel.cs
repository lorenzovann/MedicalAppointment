using MedicalAppointment.Persistance.Models.Insurance.NetworkTypeCRUD;

namespace MedicalAppointmentWeb.Api.Models.NetworkTypeWebModel
{
    public class NetworkTypeGetAllModel : BaseModel
    {
        public List<NetworkTypeModel> data {  get; set; }
    }
}
