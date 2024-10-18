using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointment.Domain.Base
{
    public interface IBaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
