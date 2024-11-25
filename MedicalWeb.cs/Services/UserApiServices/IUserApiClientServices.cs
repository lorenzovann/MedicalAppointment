using MedicalWeb.cs.Models.BaseModel;

namespace MedicalWeb.cs.Services.UserApiServices
{
    public interface IUserApiClientServices
    {
        Task<UserGetAllModel> GetUser(); 

    }
}
