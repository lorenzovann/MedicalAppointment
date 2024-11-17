
namespace MedicalCoreAplications.cs.Dtos.Configurations.DoctorDtos
{
    public class BaseDoctorDtos
    {
        public string? NameDoctor { get; set; }
        public int SpecialtyID { get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ClinicAddress { get; set; }
       
       
    }
}
