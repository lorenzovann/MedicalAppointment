

namespace MedicalAppoiment.Aplication.cs.Dtos.Systems.RoleDtos.cs
{
    public class getRolesDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
