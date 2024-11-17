

namespace MedicalCoreAplications.cs.Dtos.Configurations.DoctorDtos
{
    public sealed class UpdateDoctorDtos : BaseDoctorDtos
    {
        public int DoctorID { get; set; }
        public bool IsActive { get; set; }
    }
}
