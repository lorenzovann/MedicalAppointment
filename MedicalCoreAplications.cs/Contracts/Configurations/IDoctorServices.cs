

using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Configuration.DoctorDtos.cs;
using MedicalCoreAplications.cs.Response.Configurations;

namespace MedicalCoreAplications.cs.Contracts.Configurations
{
    public interface IDoctorServices : IBaseServices<DoctorResponse, SaveDoctorDtos, UpdateDoctorDtos>
    {  

    }
}
