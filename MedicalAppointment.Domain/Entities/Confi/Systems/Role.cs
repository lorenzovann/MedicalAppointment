
using Medical.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Medical.Domain.Entities.Confi.Systems
{
    [Table("Roles", Schema = "system")]
    public sealed class Role : BaseEntitie
    {
        [Key]
        public int RoleID{ get; set; }
        public string RoleName { get; set; }
    }
}