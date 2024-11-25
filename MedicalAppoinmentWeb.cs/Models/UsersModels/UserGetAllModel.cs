using MedicalAppointment.Persistance.Model;
using MedicalWeb.cs.Models.Base;

namespace MedicalWeb.cs.Models.BaseModel
{
    public class UserGetAllModel : BaseApiResponse
    {
        public List<UserModel> data { get; set; }
    }
}