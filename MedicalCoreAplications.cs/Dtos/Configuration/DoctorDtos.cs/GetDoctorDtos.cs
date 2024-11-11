
namespace MedicalCoreAplications.cs.Dtos.Configuration.DoctorDtos.cs
{
    public class GetDoctorDtos
    { 
        public int DoctorID { get; set; }
        public string? NameDoctor { get; set; }
        public int SpecialtyID { get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string? ClinicAddress { get; set; }
        public bool IsActive { get; set; } 
        public DateTime? UpdateAt { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
