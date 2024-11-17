using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCoreAplications.cs.Dtos.Configurations.PatientDtos
{
    public sealed class UpdatePatientsDtos : BasePatientsDto
    {
        public int PatientID { get; set; }
    }
}
