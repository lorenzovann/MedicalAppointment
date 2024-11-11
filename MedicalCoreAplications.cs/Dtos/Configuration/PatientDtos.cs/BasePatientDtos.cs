

namespace MedicalCoreAplications.cs.Dtos.Configuration.PatientDtos.cs
{
    public abstract class BasePatientDtos 
    {

        public string NamePatient { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
    }
}
