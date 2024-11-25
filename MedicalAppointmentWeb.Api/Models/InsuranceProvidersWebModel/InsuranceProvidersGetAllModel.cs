using MedicalAppointment.Persistance.Models.Insurance.InsuranceProvidersCRUD;

namespace MedicalAppointmentWeb.Api.Models.InsuranceProvidersWebModel
{
    public class InsuranceProvidersGetAllModel : BaseModel
    {
        public List<InsuranceProvidersModel> data {  get; set; }
    }
}
