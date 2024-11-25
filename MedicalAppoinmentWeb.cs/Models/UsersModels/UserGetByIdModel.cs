using MedicalAppointment.Persistance.Model;
using MedicalWeb.cs.Models.Base;

namespace MedicalWeb.cs.Models.BaseModel
{
    public class UserGetByIdModel : BaseApiResponse
    {
        public UserModel data { get; set; }


    }
}