

using MedicalAppoiment.Aplication.cs.Base;
using MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Users;
using MedicalAppoiment.Aplication.cs.Response.Users;
using MedicalAppointment.Domain.IBaseRepositorie;

namespace MedicalAppoiment.Aplication.cs.Contracts.configurations
{
    public interface IUserService : IBaseServices<UserResponse, UserSaveDto, UserUpdateDto>
    { 

    }
}
