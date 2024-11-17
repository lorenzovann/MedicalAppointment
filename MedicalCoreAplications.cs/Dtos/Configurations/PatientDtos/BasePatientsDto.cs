
namespace MedicalCoreAplications.cs.Dtos.Configurations.PatientDtos
{
    public class BasePatientsDto 
    {
        public string? NamePatient { get; set; }
        public DateTime DateofBirth { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public int InsuranceProviderID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
