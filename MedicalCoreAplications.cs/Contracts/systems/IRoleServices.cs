using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Systems.RolesDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCoreAplications.cs.Contracts.Systems
{
    public interface IRoleServices : IBaseServices<RoleResponse, SaveRolesDtos, UpdateRolesDtos, GetRolesDtos>
    {
    }
}
