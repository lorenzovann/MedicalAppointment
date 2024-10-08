

using Medical.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointment.Domain.Entities.Confi.User
{
    [Table("Users", Schema = "User")]
    public class User : BaseEntitie
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
            