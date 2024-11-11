

using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Configuration.PatientDtos.cs;
using MedicalCoreAplications.cs.Response.Configurations;

namespace MedicalCoreAplications.cs.Contracts.Configurations
{
    public interface IPatientsServices : IBaseServices<PatientResponse, SavePatientsDtos, UpdatePatientsDtos>
    { 


    }
}
