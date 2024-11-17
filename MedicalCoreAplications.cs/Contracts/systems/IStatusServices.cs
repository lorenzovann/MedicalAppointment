

using MedicalCoreAplications.cs.Base;
using MedicalCoreAplications.cs.Dtos.Systems.StatusDtos.cs;
using MedicalCoreAplications.cs.Response.Systems;

namespace MedicalCoreAplications.cs.Contracts.Systems
{
    public interface IStatusServices : IBaseServices<StatusResponse, SaveStatusDtos, UpdateStatusDtos, GetStatusDtos>
    {
    }
}
