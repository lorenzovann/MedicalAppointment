

using Medical.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Domain.Entities.Confi.Systems
{
    [Table("Status", Schema = "system")]
    public sealed class Status 
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}

