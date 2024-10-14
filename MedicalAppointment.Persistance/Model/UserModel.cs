

namespace MedicalAppointment.Persistance.Model
{
    public class UserModel
    {
        public int IDUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pasword { get; set; }
        public int RoleId { get; set; }
    }
}
