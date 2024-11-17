

namespace MedicalCoreAplications.cs.Dtos.Configurations.PatientDtos
{
    public class GetPatientDtos 
    {
        public int PatientID { get; set; }
        public string? NamePatient { get; set; }
        public DateTime DateofBirth { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public int InsuranceProviderID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
