

using Medical.Domain.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointment.Domain.Entities.Confi.User
{
    [Table("Patients", Schema = "User")]
    public sealed class Patient : BaseEntitie
    {
        [Key]
        public int PatiendID { get; set; } 
        public string? Name { get; set; }  
        public DateTime DateofBirth { get; set; }   
        public char Gender { get; set; }    
        public string Address { get; set; }  
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }    

    }
}
                           