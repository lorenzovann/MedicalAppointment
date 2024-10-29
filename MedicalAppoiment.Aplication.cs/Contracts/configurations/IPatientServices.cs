

using MedicalAppoiment.Aplication.cs.Base;
using MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.PatientsDtos.cs;
using MedicalAppoiment.Aplication.cs.Response.Users;
using MedicalAppointment.Domain.IBaseRepositorie;

namespace MedicalAppoiment.Aplication.cs.Contracts.configurations
{
    public interface IPatientServices : IBaseServices<PatientResponse, SavePatientsDto, PatientsUpdpateDto>
    { 

    }
}
