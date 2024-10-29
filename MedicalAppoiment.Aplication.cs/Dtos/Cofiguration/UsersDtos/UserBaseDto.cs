namespace MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Users
{
    public class UserBaseDto
    { 

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime fechaCambio { get; set; }
        public int UserChange { get; set; }

    }
}
