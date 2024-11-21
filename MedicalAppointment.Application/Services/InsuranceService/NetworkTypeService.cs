

using MedicalAppointment.Application.Contracts.InsuranceContracts;
using MedicalAppointment.Application.Dto.DtosInsurance.NetworkTypeDtos;
using MedicalAppointment.Application.Responses.InsuranceResponses;
using MedicalAppointment.Persistance.Interfaces.Insurance;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.InsuranceService
{
    public class NetworkTypeService : INetworkTypeService
    {
        private readonly INetworkTypeRepository _networkTypeRepository;
        private readonly ILogger<NetworkTypeService> _logger;

        
        public NetworkTypeService(INetworkTypeRepository networkTypeRepository,
                                   ILogger<NetworkTypeService> logger) 
        { 
            _networkTypeRepository = networkTypeRepository;
            _logger = logger;
        
        }

        public Task<NetworkTypeResponse> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<NetworkTypeResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<NetworkTypeResponse> SaveAsync(NetworkTypeSaveDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<NetworkTypeResponse> UpdateAsync(NetworkTypeUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
