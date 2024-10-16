

using Medical.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Domain.Entities.Confi.Systems
{
    [Table("Status", Schema = "Systems")]
    public sealed class Status 
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}

