
using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Configurations.PatientDtos;
using MedicalCoreAplications.cs.Response.Configurations;

namespace MedicalCoreAplications.cs.Contracts.Configurations
{
    public interface IPatientServices : IBaseServices<PatientResponse, SavePatientsDtos, UpdatePatientsDtos, GetPatientDtos>
    {

    }
}
