using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppoiment.Infraectructure.Core
{
    public class NotificationsResult
    {
        public string? Mensaje { get; set; }
        public dynamic? data { get; set; }
        public bool success  { get; set; } 
        public NotificationsResult() { this.success = true; }
    }
}
