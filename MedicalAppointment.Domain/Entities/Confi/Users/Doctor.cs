

using Medical.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Domain.Entities.Confi.Users
{
    [Table("Doctors", Schema = "users")] // mapeo 
    public sealed class Doctor : BaseEntitie
    {

        [Key]
        public int DoctorID { get; set; } 
        public string? NameDoctor { get; set; }
        public int SpecialtyID { get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public string? Bio { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public int? AvailabilityModeId { get; set; }
        public DateTime LicenseExpirationDate { get; set; }

    }
}