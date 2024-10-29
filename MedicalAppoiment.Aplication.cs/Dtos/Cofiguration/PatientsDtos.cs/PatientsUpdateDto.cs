using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.PatientsDtos.cs
{
    public class PatientsUpdpateDto : BasePatientsDto
    {
        public int PatientID { get; set; }
        public bool IsActive { get; set; }
       
    }
}
