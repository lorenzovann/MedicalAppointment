

using MedicalAppointment.Domain.Base;

namespace MedicalAppointment.Domain.Entities.Confi.User

{
    public sealed class UserDoctor : BaseEntity
    { 
        public int IDDoctor { get; set; } 
        public int SpecialtyID { get; set; }    
        public string LicenseNumber { get; set; }   
        public int YearofExperinces {  get; set; }  
        public string Education {  get; set; }  
        public string? Bio {  get; set; }   
        public decimal? ConsultacionFee { get; set; }   
        public string? ClinicAdress { get; set; }    
        public int? AvailabilityModeId { get; set; }
        public DateTime LicenseExpirationDate { get; set; } 

    }
}
