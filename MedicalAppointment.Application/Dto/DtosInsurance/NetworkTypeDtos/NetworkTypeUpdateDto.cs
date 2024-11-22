

namespace MedicalAppointment.Application.Dto.DtosInsurance.NetworkTypeDtos
{
    public class NetworkTypeUpdateDto : IBaseDtoForTwo
    {
        public int NetworkTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
