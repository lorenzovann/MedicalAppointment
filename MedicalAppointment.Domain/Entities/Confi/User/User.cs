
using MedicalAppointment.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointment.Domain.Entities.Confi.User

{
    public class User : BaseEntity
    {
        [Key]
        public int IDUser {  get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string Pasword { get; set; } 
        public int RoleId { get; set; } 

    }
}
