
using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Systems.RolesDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;

namespace MedicalCoreAplications.cs.Contracts.systems
{
    public interface IRoleServices : IBaseServices<RoleResponse, SaveRolesDtos, RolesUpdateDtos>
    {
    }
}
