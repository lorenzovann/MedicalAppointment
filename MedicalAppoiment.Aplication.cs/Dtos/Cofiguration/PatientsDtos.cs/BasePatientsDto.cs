using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.PatientsDtos.cs
{
    public class BasePatientsDto
    {
   
        public string NamePatient { get; set; }
        public DateTime DateofBirth { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
