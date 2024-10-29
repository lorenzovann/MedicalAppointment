

namespace MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Doctors
{
    public class DoctorUpdateDto : BaseDoctorDto
    {
        public int DoctorID { get; set; }
        public bool IsActive { get; set; }
    }
}
