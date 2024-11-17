using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCoreAplications.cs.Dtos.Systems.NotificationsDtos.cs
{
    public class BaseNotifications
    {
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
