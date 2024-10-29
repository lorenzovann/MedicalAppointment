using MedicalAppoiment.Aplication.cs.Base;
using MedicalAppoiment.Aplication.cs.Dtos.Systems.RoleDtos.cs;
using MedicalAppoiment.Aplication.cs.Response.systems;

namespace MedicalAppoiment.Aplication.cs.Contracts.systems
{
    public interface IRoleServices : IBaseServices<RoleResponses, SaveRolesDtos, RoleUpdateDtos>
    {
    }
}
