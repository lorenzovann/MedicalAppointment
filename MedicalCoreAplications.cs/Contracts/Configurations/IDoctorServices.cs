using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Configurations.DoctorDtos;
using MedicalCoreAplications.cs.Response.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCoreAplications.cs.Contracts.Configurations
{
    public interface IDoctorServices : IBaseServices<DoctorResponse, SaveDoctorDtos, UpdateDoctorDtos, GetDoctorDtos>
    {
    }
}
