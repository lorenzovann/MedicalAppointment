

using MedicalAppoiment.Aplication.cs.Base;
using MedicalAppoiment.Aplication.cs.Dtos.Systems.StatusDtos.cs;
using MedicalAppoiment.Aplication.cs.Response.systems;

namespace MedicalAppoiment.Aplication.cs.Contracts.systems
{
    public interface IStatusServices : IBaseServices<StatusResponses, StatusSaveDtos, StatusUpdateDtos>
    {

    }
}
