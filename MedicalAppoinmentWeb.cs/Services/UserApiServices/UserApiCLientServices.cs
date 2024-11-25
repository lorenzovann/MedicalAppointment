using MedicalWeb.cs.Models.BaseModel;
using MedicalWeb.cs.Services.Base;
using MedicalWeb.cs.Services.BaseServices;

namespace MedicalWeb.cs.Services.UserApiServices
{
    public class UsersApiClientServices : IUserApiClientServices
    {


        public readonly IHttpServices httpServices;
        private readonly ILogger<UsersApiClientServices> _logger;
        private readonly IConfiguration _configurations;

        public UsersApiClientServices(IHttpServices httpServices,
                                      ILogger<UsersApiClientServices> logger)
        {
            this.httpServices = httpServices;
            _logger = logger;

        }
        public async Task<UserGetAllModel> GetUser()
        {
            UserGetAllModel result = new UserGetAllModel();

            try
            {
                result = await httpServices.GetAsync<UserGetAllModel>("Users/GetUsers");
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.menssage = ex.Message;
                _logger.LogError($"{result.menssage} {ex.ToString()}");

            }

            return result;
        }



    }
}