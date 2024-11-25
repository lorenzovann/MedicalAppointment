using MedicalAppointment.Persistance.Models.Insurance.InsuranceProvidersCRUD;

namespace MedicalAppointmentWeb.Api.Models.InsuranceProvidersWebModel
{
    public class InsuranceProvidersGetByIdModel : BaseModel
    {
        public InsuranceProvidersModel data { get; set; }
    }
}
