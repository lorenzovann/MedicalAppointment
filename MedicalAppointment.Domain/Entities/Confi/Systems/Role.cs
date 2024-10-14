
using Medical.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Medical.Domain.Entities.Confi.Systems
{
    [Table("Role", Schema = "System")]
    public sealed class Role : BaseEntitie
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}