using MedicalAppointment.Persistance.Model;

namespace MedicalWeb.cs.Models.BaseModel
{
    public class UserGetAllModel : BaseApiResponse
    {
        public List<UserModel> data { get; set; }
    }
}
