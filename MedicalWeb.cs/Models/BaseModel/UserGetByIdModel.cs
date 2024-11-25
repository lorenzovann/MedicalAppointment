using MedicalAppointment.Persistance.Model;

namespace MedicalWeb.cs.Models.BaseModel
{
    public class UserGetByIdModel : BaseApiResponse
    { 
        public UserModel data { get; set; }  


    }
}
