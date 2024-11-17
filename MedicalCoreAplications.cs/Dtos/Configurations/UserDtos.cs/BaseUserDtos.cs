

namespace MedicalCoreAplications.cs.Dtos.Configurations.UserDtos.cs
{
    public class BaseUserDtos 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
