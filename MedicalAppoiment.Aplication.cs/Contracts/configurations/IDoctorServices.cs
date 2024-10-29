

using MedicalAppoiment.Aplication.cs.Base;
using MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Doctors;
using MedicalAppoiment.Aplication.cs.Response.Users;

namespace MedicalAppoiment.Aplication.cs.Contracts.configurations
{
    public interface IDoctorServices : IBaseServices<DoctorResponse, DoctorSaveDto, DoctorUpdateDto>
    { 

    }
}
