namespace MedicalAppoiment.Aplication.cs.Dtos.Cofiguration.Users
{
    public class UserUpdateDto : UserBaseDto
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }

    }
}
