

using Medical.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Domain.Entities.Confi.Systems
{
    [Table("Notifications", Schema = "System")]
    public sealed class Notifications 
    {

        [Key]
        public int NotificationId { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }

    }
}
       
