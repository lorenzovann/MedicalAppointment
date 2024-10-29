using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppoiment.Aplication.cs.Dtos.Systems.RoleDtos.cs
{
    public class RoleUpdateDtos : RoleBaseDtos
    {
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
    }
}
