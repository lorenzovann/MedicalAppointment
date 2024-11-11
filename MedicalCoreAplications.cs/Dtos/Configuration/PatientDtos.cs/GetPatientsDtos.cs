

namespace MedicalCoreAplications.cs.Dtos.Configuration.PatientDtos.cs
{
    public class GetPatientsDtos
    {

        public int PatientID { get; set; }
        public string NamePatient { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
