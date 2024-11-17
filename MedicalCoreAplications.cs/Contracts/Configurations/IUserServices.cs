using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Configurations.UserDtos.cs;
using MedicalCoreAplications.cs.Response.Configurations;


namespace MedicalCoreAplications.cs.Contracts.Configurations
{
    public interface IUserServices : IBaseServices<UserResponse, SaveUserDtos, UpdateUserDtos, GetUserDtos>
    {
    }
}
